using System.Collections.Generic;

namespace GUtils.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that is satisifed when all the child predicates are satisfied.
    /// </summary>
    /// <inheritdoc />
    public sealed class AndPredicate : IPredicate
    {
        readonly IReadOnlyCollection<IPredicate> _conditions;

        public AndPredicate(
            IReadOnlyCollection<IPredicate> conditions
        )
        {
            _conditions = conditions;
        }

        public bool IsSatisfied()
        {
            foreach (IPredicate condition in _conditions)
            {
                if (!condition.IsSatisfied())
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// A <see cref="IPredicate"/> that is satisifed when all the child predicates are satisfied.
    /// </summary>
    // <inheritdoc />
    public sealed class AndPredicate<T> : IPredicate<T>
    {
        readonly IReadOnlyCollection<IPredicate<T>> _conditions;

        public AndPredicate(
            IReadOnlyCollection<IPredicate<T>> conditions
        )
        {
            _conditions = conditions;
        }

        public bool IsSatisfied(T arg)
        {
            foreach (IPredicate<T> condition in _conditions)
            {
                if (!condition.IsSatisfied(arg))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
