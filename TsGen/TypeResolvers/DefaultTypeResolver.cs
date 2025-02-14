﻿using TsGen.Interfaces;
using TsGen.Models;

namespace TsGen.TypeResolvers
{
    /// <summary>
    /// The default property type resolver
    /// </summary>
    /// <remarks>
    /// This type resolver combines some of the built-in resolvers to build a sensible default resolver that should handle most scenarios.
    /// 
    /// The order of resolution is:
    /// <list type="number">
    /// <item>
    /// <term><see cref="BuiltInTypeResolver"/> which handles primitives and a few basic built-in types.</term>
    /// </item>
    /// <item>
    /// <term><see cref="CollectionTypeResolver"/> which handles most built-in collections (including generics) and any collection that implements IEnumerable.</term>
    /// </item>
    /// <item>
    /// <term><see cref="DateTimeTypeResolver"/> which handles the various date and time types.</term>
    /// </item>
    /// <item>
    /// <term><see cref="GenericTypeResolver"/> which handles any generic types not already handled in the collection resolver.</term>
    /// </item>
    /// <item>
    /// <term><see cref="ObjectTypeResolver"/> which resolves any remaining types as the name of the type and flags that type for generation also.</term>
    /// </item>
    /// </list>
    /// </remarks>
    public class DefaultTypeResolver : ITypeResolver
    {
        private readonly BuiltInTypeResolver _builtInResolver = new();
        private readonly CollectionTypeResolver _collectionResolver = new();
        private readonly GenericTypeResolver _genericResolver = new();
        private readonly DateTimeTypeResolver _dateTimeResolver = new();
        private readonly ObjectTypeResolver _objectResolver = new();

        /// <summary>
        /// Attempts to resovlve a Typescript type from the passed in type
        /// </summary>
        /// <param name="type">The type to attempt to resolve.</param>
        /// <param name="optional">Whether or not the resolved type should be optional.</param>
        /// <param name="recursiveResolver">The recursive resolver for resolving nested types.</param>
        /// <returns>A resolved type if the type can be handled by this resolver (see list in class description) otherwise null.</returns>
        public ResolvedType? Resolve(Type type, bool optional, ITypeResolver recursiveResolver)
            => _builtInResolver.Resolve(type, optional, recursiveResolver)
                ?? _collectionResolver.Resolve(type, optional, recursiveResolver)
                ?? _dateTimeResolver.Resolve(type, optional, recursiveResolver)
                ?? _genericResolver.Resolve(type, optional, recursiveResolver)
                ?? _objectResolver.Resolve(type, optional, recursiveResolver);
            
    }
}
