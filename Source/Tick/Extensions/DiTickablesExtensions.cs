using System;
using GUtils.Di.Builder;
using GUtils.Executables;
using GUtils.Tick.Enums;
using GUtils.Tick.Services;
using GUtils.Tick.Tickables;

namespace GUtils.Tick.Extensions
{
    public static class DiTickablesExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            TickType tickType = TickType.Update
            )
            where T : ITickable
        {
            actionBuilder.WhenInit((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(o, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.RemoveNow(o, tickType);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<T, Action> func,
            TickType tickType = TickType.Update
            )
        {
            CallbackTickable? callbackTickable = null;

            actionBuilder.WhenInit((c, o) =>
            {
                Action action = func.Invoke(o);

                callbackTickable = new CallbackTickable(action);
                ITickablesService tickablesService = c.Resolve<ITickablesService>();
                tickablesService.Add(callbackTickable, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();
                tickablesService.RemoveNow(callbackTickable!, tickType);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
        
        public static IDiBindingActionBuilder<T> LinkExecutableToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            TickType tickType = TickType.Update
        ) where T : IExecutable
        {
            return actionBuilder.LinkToTickablesService(o => o.Execute, tickType);
        }
    }
}
