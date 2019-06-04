using System;

namespace RESTServiceMeassurement
{
    public class Meassurement
    {
        public int MesId { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public DateTime TimeStamp;

        public Meassurement(int pressure, int humidity, int temperature)
        {
            Pressure = pressure;
            Humidity = humidity;
            Temperature = temperature;
        }

        public  Meassurement() { }

        public override string ToString()
        {
            return $"{nameof(TimeStamp)}: {TimeStamp}, {nameof(MesId)}: {MesId}, {nameof(Pressure)}: {Pressure}, {nameof(Humidity)}: {Humidity}, {nameof(Temperature)}: {Temperature}";
        }
    }
}