using UnityEngine;

namespace SleekRender
{
    public class DualFilterBloomRenderer
    {
        private RenderTexture[] _blurTextures;
        private Material _brightpassBlurMaterial;
        private Material _downsampleBlurMaterial;
        private PassRenderer _renderer;

        private int _bloomPasses;
        private int _baseTextureHeight;
        private int _baseTextureWidth;
        private bool _preserveAspectRatio;

        public DualFilterBloomRenderer(PassRenderer renderer)
        {
            _renderer = renderer;
        }

        public RenderTexture ApplyToAndReturn(RenderTexture source, SleekRenderSettings settings)
        {
            Vector4 luminanceThreshold = HelperExtensions.GetLuminanceThreshold(settings);

            // Changing current Luminance Const value just to make sure that we have the latest settings in our Uniforms
            _brightpassBlurMaterial.SetVector(Uniforms._LuminanceThreshold, luminanceThreshold);
            _brightpassBlurMaterial.SetVector(Uniforms._TexelSize, new Vector2(1f / _blurTextures[0].width, 1f / _blurTextures[0].height));

            var currentTargetRenderTexture = _blurTextures[0];
            var previousTargetRenderTexture = _blurTextures[0];
            var passes = _blurTextures.Length;
            for (int i = 0; i < passes; i++)
            {
                currentTargetRenderTexture = _blurTextures[i];

                // We use a different material for the first blur pass
                if (i == 0)
                {
                    // Applying downsample + brightpass (stored in Alpha)
                    _renderer.Blit(source, currentTargetRenderTexture, _brightpassBlurMaterial);
                }
                else
                {
                    _downsampleBlurMaterial.SetVector(Uniforms._TexelSize, new Vector2(1f / currentTargetRenderTexture.width, 1f / currentTargetRenderTexture.height));
                    // Applying only blur to our already brightpassed texture
                    _renderer.Blit(previousTargetRenderTexture, currentTargetRenderTexture, _downsampleBlurMaterial);
                }

                previousTargetRenderTexture = currentTargetRenderTexture;
            }

            return currentTargetRenderTexture;
        }

        public void CreateResources(SleekRenderSettings settings, Camera camera)
        {
            _bloomPasses = settings.bloomPasses;
            _preserveAspectRatio = settings.preserveAspectRatio;
            CalculateBloomHeightAndWidth(settings, camera);

            switch (_bloomPasses)
            {
                case 3:
                    _blurTextures = new RenderTexture[3];
                    _blurTextures[0] = HelperExtensions.CreateTransientRenderTexture("Brightpass Blur 0", 
                        _baseTextureWidth, _baseTextureHeight);

                    _blurTextures[1] = HelperExtensions.CreateTransientRenderTexture("Downsample Blur 1", 
                        _baseTextureWidth / 2, _baseTextureHeight / 2);

                    _blurTextures[2] = HelperExtensions.CreateTransientRenderTexture("Downsample Blur 2", 
                        _baseTextureWidth, _baseTextureHeight);
                    break;
                default:
                    _blurTextures = new RenderTexture[5];
                    _blurTextures[0] = HelperExtensions.CreateTransientRenderTexture("Brightpass Blur 0", 
                        _baseTextureWidth, _baseTextureHeight);

                    _blurTextures[1] = HelperExtensions.CreateTransientRenderTexture("Downsample Blur 1", 
                        _baseTextureWidth / 2, _baseTextureHeight / 2);

                    _blurTextures[2] = HelperExtensions.CreateTransientRenderTexture("Downsample Blur 2", 
                        _baseTextureWidth / 4, _baseTextureHeight / 4);

                    _blurTextures[3] = HelperExtensions.CreateTransientRenderTexture("Downsample Blur 4", 
                        _baseTextureWidth / 2, _baseTextureHeight / 2);

                    _blurTextures[4] = HelperExtensions.CreateTransientRenderTexture("Downsample Blur 4", 
                        _baseTextureWidth, _baseTextureHeight);
                    break;
            }

            _brightpassBlurMaterial = HelperExtensions.CreateMaterialFromShader("Sleek Render/Post Process/Brightpass Dualfilter Blur");
            _downsampleBlurMaterial = HelperExtensions.CreateMaterialFromShader("Sleek Render/Post Process/Downsample Dualfilter Blur");
        }

        private void CalculateBloomHeightAndWidth(SleekRenderSettings settings, Camera camera)
        {
            _baseTextureHeight = settings.bloomTextureHeight;

            if(settings.preserveAspectRatio)
            {
                _baseTextureWidth = Mathf.RoundToInt(_baseTextureHeight * camera.aspect);
            }
            else
            {
                _baseTextureWidth = settings.bloomTextureWidth;
            }
        }

        public bool SomeSettingsHaveChanged(SleekRenderSettings settings)
        {
            var textureWidthIsWrong = settings.preserveAspectRatio ? false : settings.bloomTextureWidth != _baseTextureWidth;

            return settings.bloomPasses != _bloomPasses
                || settings.bloomTextureHeight != _baseTextureHeight
                || settings.preserveAspectRatio != _preserveAspectRatio
                || textureWidthIsWrong;
        }

        public void ReleaseResources()
        {
            if (_blurTextures != null)
            {
                foreach (var blurTexture in _blurTextures)
                {
                    blurTexture.DestroyImmediateIfNotNull();
                }
            }

            _blurTextures = null;

            _brightpassBlurMaterial.DestroyImmediateIfNotNull();
            _downsampleBlurMaterial.DestroyImmediateIfNotNull();
        }
    }
}
