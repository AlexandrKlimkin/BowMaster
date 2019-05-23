
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UI.Markers {

    public class MarkerManager : SingletonBehaviour<MarkerManager> {

        private static List<MarkerProvider> _MarkerProviders = new List<MarkerProvider>();

        private List<MarkerWidget> _MarkerWidgets = new List<MarkerWidget>();

        public static void ResetWidgets() {
            Instance._MarkerWidgets.ForEach(_ => DestroyImmediate(_.gameObject));
            Instance._MarkerWidgets = new List<MarkerWidget>();
            _MarkerProviders.ForEach(_ => OnProviderVisibilityChanged(_));
        }

        public static void RegisterProvider(MarkerProvider provider) {
            if (_MarkerProviders.Contains(provider))
                return;
            _MarkerProviders.Add(provider);
            provider.OnVisibilityChanged += OnProviderVisibilityChanged;
            OnProviderVisibilityChanged(provider);
        }

        public static void UnregisterProvider(MarkerProvider provider) {
            _MarkerProviders.Remove(provider);
            provider.OnVisibilityChanged -= OnProviderVisibilityChanged;
        }

        private static void OnProviderVisibilityChanged(MarkerProvider provider) {
            if (provider.Visible) {
                Instance.GetMarker(provider.RequiredMarkerType).AssignProvider(provider);
            }
        }

        private MarkerWidget GetMarker(Type t) {
            var marker = _MarkerWidgets.FirstOrDefault(_ => (!_.gameObject.activeSelf) && _.GetType() == t);
            if (marker == null) {
                marker = AddMarker(t);
            }
            return marker;
        }

        private MarkerWidget AddMarker(Type type) {
            var markerWidget = Instantiate(MarkerResourcesCache.GetMarker(type));
            markerWidget.transform.SetParent(this.transform);
            markerWidget.transform.localScale = Vector3.one;
            _MarkerWidgets.Add(markerWidget);
            return markerWidget;
        }

        protected override void OnEnable() {
            base.OnEnable();
            StartCoroutine(UpdateMarkerProviders());
        }

        private static IEnumerator UpdateMarkerProviders() {
            while (true) {
                for (var i = 0; i < _MarkerProviders.Count; i++) {
                    _MarkerProviders[i].UpdateProvider();
                    if (i % 50 == 0) {
                        yield return null;
                    }
                }
                yield return null;
            }
        }
    }
}