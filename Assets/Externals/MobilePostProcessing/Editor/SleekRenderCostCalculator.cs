using System.Text;

namespace SleekRender
{
    public static class SleekRenderCostCalculator
    {
        private static StringBuilder _sb = new StringBuilder();

        public static string GetTotalCostStringFor(SleekRenderSettings settings)
        {
            _sb.Remove(0, _sb.Length);
            _sb.Append("This info is VERY approximate and depends on target GPU architecture. Treat it as general performance overhead.\n\n");

            _sb.Append("Worst case (HiRez Low End GPU 2011-ish OpenGL ES 2.0 devices):\n\n");
            _sb.Append("\tBase overhead:\t2 ms\n");

            float totalCost = 2f;
            if (settings.bloomEnabled)
            {
                switch (settings.bloomPasses)
                {
                    case 3:
                        _sb.Append("\tBloom:\t\t3 ms\n");
                        totalCost += 3f;
                        break;
                    default:
                        _sb.Append("\tBloom:\t\t5 ms\n");
                        totalCost += 5f;
                        break;
                }
            }
            if (settings.colorizeEnabled)
            {
                _sb.Append("\tColorize:\t\t3 ms\n");
                totalCost += 3f;
            }
            if (settings.vignetteEnabled)
            {
                _sb.Append("\tVignette:\t\t0.5 ms\n");
                totalCost += 0.5f;
            }
            if (settings.brightnessContrastEnabled)
            {
                _sb.Append("\tBr./Contr:\t0.5ms\n");
                totalCost += 0.5f;
            }

            _sb.Append("\tTotal:\t\t" + totalCost.ToString("F2") + " ms\n\n");

            _sb.Append("General case (Arm Mali400 GPU with 480x864 screen resolution - Galaxy S2-ish):\n\n");

            _sb.Append("\tBase overhead:\t2 ms\n");
            totalCost = 2f;
            if (settings.bloomEnabled)
            {
                switch (settings.bloomPasses)
                {
                    case 3:
                        _sb.Append("\tBloom:\t\t1 ms\n");
                        totalCost += 1f;
                        break;
                    default:
                        _sb.Append("\tBloom:\t\t2 ms\n");
                        totalCost += 2f;
                        break;
                }
            }
            if (settings.colorizeEnabled)
            {
                _sb.Append("\tColorize:\t\t0.2 ms\n");
                totalCost += 0.2f;
            }
            if (settings.vignetteEnabled)
            {
                _sb.Append("\tVignette:\t\t0.2 ms\n");
                totalCost += 0.2f;
            }
            if (settings.brightnessContrastEnabled)
            {
                _sb.Append("\tBr./Contr:\t0.2ms\n");
                totalCost += 0.2f;
            }

            _sb.Append("\tTotal:\t\t" + totalCost.ToString("F2") + " ms\n\n");

            _sb.Append("Render target switch count (less is better):\n\n");

            _sb.Append("\tBase pipeline:\t4\n");
            int totalRenderTargetSwitchCount = 4;
            if (settings.bloomEnabled)
            {
                switch (settings.bloomPasses)
                {
                    case 3:
                        _sb.Append("\tBloom:\t\t3\n");
                        totalRenderTargetSwitchCount += 3;
                        break;
                    default:
                        _sb.Append("\tBloom:\t\t5\n");
                        totalRenderTargetSwitchCount += 5;
                        break;
                }
            }

            _sb.Append("\tTotal:\t\t" + totalRenderTargetSwitchCount.ToString("D") + "\n\n");

            return _sb.ToString();
        }
    }
}