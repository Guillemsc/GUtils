using System;
using GUtils.Di.Container;

namespace GUtils.Di.Bindings
{
    public sealed class NewInstanceBinding : DiBinding
    {
        public NewInstanceBinding(Type identifierType, Type actualType) : base(identifierType, actualType)
        {

        }

        protected override object OnBind(IDiResolveContainer container)
        {
            return Activator.CreateInstance(ActualType);
        }
    }
}
