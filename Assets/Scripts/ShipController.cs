using UnityEngine;

namespace Assets.Scripts
{
    public class ShipController : MonoBehaviour
    {
        [Header("Particle Effects")]
        public ParticleSystem smokeEffect;

        [Header("Smoke Settings")]
        public bool playOnStart = true;
        public Vector3 smokeLocalPosition = new Vector3(0, -0.8f, 0); // Đặt ở đuôi tàu

        void Start()
        {
            SetupSmokeEffect();
        }

        void SetupSmokeEffect()
        {
            if (smokeEffect != null)
            {
                // Đặt khói làm con của tàu nếu chưa phải
                if (smokeEffect.transform.parent != transform)
                {
                    smokeEffect.transform.SetParent(transform);
                }

                // Đặt vị trí khói
                smokeEffect.transform.localPosition = smokeLocalPosition;

                // Đảm bảo khói được kích hoạt
                smokeEffect.gameObject.SetActive(true);

                // Phát khói nếu được thiết lập
                if (playOnStart)
                {
                    PlaySmoke();
                }
            }
            else
            {
                Debug.LogWarning("Smoke Effect is not assigned in " + gameObject.name);
            }
        }

        /// <summary>
        /// Bắt đầu phát khói
        /// </summary>
        public void PlaySmoke()
        {
            if (smokeEffect != null && !smokeEffect.isPlaying)
            {
                smokeEffect.Play();
                Debug.Log("Smoke effect started");
            }
        }

        /// <summary>
        /// Dừng phát khói
        /// </summary>
        public void StopSmoke()
        {
            if (smokeEffect != null && smokeEffect.isPlaying)
            {
                smokeEffect.Stop();
                Debug.Log("Smoke effect stopped");
            }
        }

        /// <summary>
        /// Tạm dừng khói
        /// </summary>
        public void PauseSmoke()
        {
            if (smokeEffect != null)
            {
                smokeEffect.Pause();
            }
        }

        /// <summary>
        /// Tiếp tục phát khói sau khi tạm dừng
        /// </summary>
        public void ResumeSmoke()
        {
            if (smokeEffect != null)
            {
                smokeEffect.Play();
            }
        }

        /// <summary>
        /// Thay đổi vị trí khói
        /// </summary>
        /// <param name="newPosition">Vị trí mới (local position)</param>
        public void SetSmokePosition(Vector3 newPosition)
        {
            smokeLocalPosition = newPosition;
            if (smokeEffect != null)
            {
                smokeEffect.transform.localPosition = smokeLocalPosition;
            }
        }

        void OnValidate()
        {
            // Cập nhật vị trí khói khi thay đổi trong Inspector
            if (smokeEffect != null && Application.isPlaying)
            {
                smokeEffect.transform.localPosition = smokeLocalPosition;
            }
        }
    }
}