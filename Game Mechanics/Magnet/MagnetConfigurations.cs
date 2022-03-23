namespace DenizYanar
{
    public class MagnetConfigurations
    {
        public EMagnetPolar Polar;
        public float Power; 
        public float Radius;

        public MagnetConfigurations(EMagnetPolar polar, float power, float radius)
        {
            Polar = polar;
            Power = power;
            Radius = radius;
        }
    }
}