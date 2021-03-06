using System;
using System.Threading;

namespace Stl.Fusion.Bridge.Internal
{
    public class PublicationStateInfoCapture : IDisposable
    {
        private static readonly AsyncLocal<PublicationStateInfoCapture?> CurrentLocal = new AsyncLocal<PublicationStateInfoCapture?>();
        private readonly PublicationStateInfoCapture? _oldCurrent;

        public static PublicationStateInfoCapture? Current => CurrentLocal.Value;
        public PublicationStateInfo? Captured { get; private set; }

        public PublicationStateInfoCapture()
        {
            _oldCurrent = CurrentLocal.Value;
            CurrentLocal.Value = this;
        }

        public void Dispose() => CurrentLocal.Value = _oldCurrent;

        public static void Capture(PublicationStateInfo publicationStateInfo)
        {
            var current = CurrentLocal.Value;
            if (current != null)
                current.Captured = publicationStateInfo;
        }
    }
}
