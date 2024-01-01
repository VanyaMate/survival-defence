using Components.Interact;
using Controllers.Actor;
using ScriptableObjects;
using UI.Progress;
using UnityEngine;

namespace Controllers.Interact
{
    public class PlayerInteractStateFactory : InteractStateFactory
    {
        private readonly IUIProgress _uiProgress;

        public PlayerInteractStateFactory(
            IActorController actorController,
            IUIProgress _uiProgress
        ) : base(actorController)
        {
            this._uiProgress = _uiProgress;
        }

        public override InteractState Create(InteractableItemType type, SO_Item item)
        {
            switch (type)
            {
                case InteractableItemType.TAKE:
                    return this._TakeInteractState(base.Create(type, item), item);
                default:
                    return base.Create(type, item);
            }
        }

        private InteractState _TakeInteractState(InteractState baseInteractState, SO_Item item)
        {
            return new InteractState()
            {
                OnStart = () =>
                {
                    this._uiProgress.SetText($"Взять {item.Title}");
                    this._uiProgress.Show();
                    baseInteractState.OnStart();
                },
                OnFinish = () =>
                {
                    this._uiProgress.Hide();
                    baseInteractState.OnFinish();
                },
                OnCancel = () =>
                {
                    this._uiProgress.Hide();
                    baseInteractState.OnCancel();
                },
                OnProcess = (float percent) =>
                {
                    this._uiProgress.Set(percent);
                    baseInteractState.OnProcess(percent);
                },
                Time = baseInteractState.Time,
                TimeToEnd = baseInteractState.TimeToEnd,
                Percent = baseInteractState.Percent,
                Process = baseInteractState.Process
            };
        }
    }
}