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

        public override InteractState Create(InteractableItemComponent component)
        {
            switch (component.Type)
            {
                case InteractableItemType.TAKE:
                    TakeInteractableItemComponent takeComponent = (component as TakeInteractableItemComponent);
                    return this._TakeInteractState(base.Create(takeComponent), takeComponent.InteractableType);
                case InteractableItemType.RECYCLING:
                    RecyclingInteractableItemComponent comp = component as RecyclingInteractableItemComponent;
                    return this._RecyclingInteractState(base.Create(comp), comp.Reciep);
                default:
                    return base.Create(component);
            }
        }

        private InteractState _TakeInteractState(InteractState baseInteractState, SO_Interactable item)
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

        private InteractState _RecyclingInteractState(InteractState baseInteractState, SO_Reciep reciep)
        {
            return new InteractState()
            {
                OnStart = () =>
                {
                    this._uiProgress.SetText($"Проивзодится {reciep.Title}");
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