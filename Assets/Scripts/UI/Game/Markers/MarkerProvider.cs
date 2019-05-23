using UnityEngine;
using System;

namespace UI.Markers {

    public abstract class MarkerProvider : MonoBehaviour {

        public event Action<MarkerProvider> OnVisibilityChanged;

        public bool Visible {
            get {
                return _Visible;
            }
            set {
                if (_Visible != value) {
                    _Visible = value;
                    if (OnVisibilityChanged != null)
                        OnVisibilityChanged(this);
                }
            }
        }
        private bool _Visible;

        protected virtual void OnEnable() {
            MarkerManager.RegisterProvider(this);
        }

        protected virtual void OnDisable() {
            MarkerManager.UnregisterProvider(this);
            Visible = false;
        }

        public virtual void UpdateProvider() {
            Visible = GetVisibility();
        }

        public abstract Type RequiredMarkerType { get; }

        public abstract MarkerData GetMarkerData();

        public abstract bool GetVisibility();
    }

    public abstract class MarkerProvider<W, D> : MarkerProvider where W : MarkerWidget<D> where D : MarkerData, new() {

        public sealed override Type RequiredMarkerType {
            get {
                return typeof(W);
            }
        }
        D Data = new D();

        protected abstract void RefreshData(D data);

        public sealed override MarkerData GetMarkerData() {
            RefreshData(Data);
            return Data;
        }
    }
}