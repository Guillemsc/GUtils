﻿using System.Collections.Generic;
using GUtils.Delegates.Animation;
using GUtils.Loading.Contexts;
using GUtils.Sequencing.Sequencer;

namespace GUtils.Loading.Services
{
    /// <inheritdoc />
    public class LoadingService : ILoadingService
    {
        readonly ISequencer _sequencer = new Sequencer();

        readonly List<TaskAnimationEvent> _beforeLoad = new();
        readonly List<TaskAnimationEvent> _afterLoad = new();

        public bool IsLoading => _sequencer.IsRunning;

        public void AddAfterLoading(TaskAnimationEvent func)
        {
            _afterLoad.Add(func);
        }

        public void AddBeforeLoading(TaskAnimationEvent func)
        {
            _beforeLoad.Add(func);
        }

        public ILoadingContext New()
        {
            return new LoadingContext(
                _sequencer,
                _beforeLoad,
                _afterLoad
            );
        }
    }
}
