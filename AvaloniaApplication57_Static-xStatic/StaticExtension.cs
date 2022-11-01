using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AvaloniaApplication57
{
    public class StaticExtension : MarkupExtension
    {
        public StaticExtension()
        {
        }

        public StaticExtension(string member)
        {
            Member = member;
        }

        [ConstructorArgument("member")]
        public string Member { get; set; }

        [DefaultValue(null)]
        public Type MemberType { get; set; }

        protected Func<object> TryGetMemberValue(IServiceProvider serviceProvider)
        {
            if (Member == null)
                throw new InvalidOperationException("Member property must be set to StaticExtension before calling ProvideValue method.");

            if (MemberType == null && serviceProvider != null)
            {
                var memberIndex = Member.IndexOf('.');
                if (memberIndex > 0)
                {
                    var typeName = Member.Substring(0, memberIndex);

                    if (XamlTypeResolver.TryResolveXamlType(serviceProvider, typeName, out Type type))
                        MemberType = type;

                    if (MemberType != null)
                    {
                        while (true)
                        {
                            Member = Member.Substring(memberIndex + 1);

                            memberIndex = Member.IndexOf('.');

                            if (memberIndex < 0) break;

                            string tempMember = Member.Substring(0, memberIndex);

                            MemberType = MemberType.GetNestedType(tempMember);

                            if (MemberType == null)
                            {
                                throw new Exception($"No nested type '{tempMember}' found in {MemberType}.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"StaticExtension: Failed to resolve {typeName} from {Member}!");
                    }
                }
            }

            if (MemberType != null)
            {
                if (StaticMemberResolver.TryResolveGetMember(MemberType, Member, out var getter))
                    return getter;
            }

            throw new ArgumentException($"Member '{Member}' could not be resolved to a static member");
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => TryGetMemberValue(serviceProvider)();

        private class XamlTypeResolver
        {
            private static Dictionary<string, Type> _cache = new Dictionary<string, Type>();

            private static bool TryResolveClrType(string fullTypeName, string assembly, out Type type)
            {
                string typeKey = $"{fullTypeName}:{assembly}";
                if (_cache.TryGetValue(typeKey, out type))
                {
                    return true;
                }

                type = null;

                if (string.IsNullOrEmpty(assembly))
                {
                    type = _cache.FirstOrDefault(v => v.Key.StartsWith(typeKey)).Value;
                    if (type == null)
                    {
                        //ah we need traverse all loaded assemblies, let's hope assembly is loaded
                        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            type = asm.GetType(fullTypeName, false);
                            if (type != null) break;
                        }
                    }
                }
                else
                {
                    type = Assembly.Load(assembly)?.GetType(fullTypeName);
                }

                if (type != null)
                {
                    _cache[typeKey] = type;
                }

                return type != null;
            }

            public static bool TryResolveXamlType(IServiceProvider serviceProvider, string xamlTypeName, out Type type)
            {
                var nsInfo = (IAvaloniaXamlIlXmlNamespaceInfoProvider)serviceProvider.GetService(typeof(IAvaloniaXamlIlXmlNamespaceInfoProvider));
                var typeData = xamlTypeName.Split(':');
                var prefix = typeData.Length > 1 ? typeData[0] : "";
                var typeName = typeData[typeData.Length - 1];

                foreach (var ns in nsInfo.XmlNamespaces[prefix])
                {
                    if (TryResolveClrType($"{ns.ClrNamespace}.{typeName}", ns.ClrAssemblyName, out type))
                    {
                        return true;
                    }
                }

                //we shouldn't get to here anyway but this is the default avalonia xaml resolving
                //it doesn't work when se use namespace without assembly name
                // support [ns:]Type.Member
                var typeResolver = (IXamlTypeResolver)serviceProvider.GetService(typeof(IXamlTypeResolver));
                type = typeResolver?.Resolve(xamlTypeName);
                return type != null;
            }
        }

        private class StaticMemberResolver
        {
            private static Dictionary<Type, Dictionary<string, Func<object>>> _cache = new Dictionary<Type, Dictionary<string, Func<object>>>();

            public static bool TryResolveGetMember(Type type, string memberName, out Func<object> getter)
            {
                if (!_cache.TryGetValue(type, out var typeMembers))
                    _cache[type] = typeMembers = new Dictionary<string, Func<object>>();

                if (typeMembers.TryGetValue(memberName, out getter))
                {
                    return true;
                }

                while (type != null && getter == null)
                {
                    getter = TryResolveGetPropertyOrFieldDirect(type, memberName);
                    type = type.BaseType;
                }

                if (getter != null)
                    typeMembers[memberName] = getter;

                return getter != null;
            }

            private static Func<object> TryResolveGetPropertyOrFieldDirect(Type type, string memberName)
            {
                var pi = type.GetRuntimeProperty(memberName);
                if (pi != null && (pi.GetGetMethod(true)?.IsStatic ?? false))
                    return () => pi.GetValue(null, null);

                var fi = type.GetRuntimeField(memberName);
                if (fi != null && fi.IsStatic)
                    return () => fi.GetValue(null);

                return null;
            }
        }
    }
}
