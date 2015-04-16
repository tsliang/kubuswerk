using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildMonitor.Models {

    [Flags]
    public enum Channel {
        Green = 64,
        Red = 128
    }

    public enum Mode {
        Off = 0,
        On = 1,
        Blink = 2,
        Pulse = 3
    }

    public class CubeConfiguration {
        public Channel Channel { get; set; }
        public Mode Mode { get; set; }

        private byte repetitions = 0;
        public byte Repetitions{
            get { return repetitions; }
            set {
                if (value > 15){
                    throw new ArgumentException("Maximum 15 repetitions.");
                }
                if (value < 0){
                    throw new ArgumentException("Repetitions cannot be negative.");
                }
                repetitions = value;
            }
        }

        public byte ToOpCode(){
            byte opCode = 0;
            opCode |= (byte) Channel;
            opCode |= Convert.ToByte((byte) Mode << 4);
            opCode |= repetitions;
            return opCode;
        }

        public static readonly CubeConfiguration AllOff = new CubeConfiguration {
            Channel = Channel.Red | Channel.Green,
            Mode = Mode.Off,
            Repetitions = 0
        };

        public static bool operator == (CubeConfiguration lhs, CubeConfiguration rhs){
            if ((object) lhs == null && (object) rhs == null){
                return true;
            }
            if (((object) lhs == null && (object) rhs != null) || ((object) lhs != null && (object) rhs == null)){
                return false;
            }
            return lhs.Channel == rhs.Channel && lhs.Mode == rhs.Mode && lhs.Repetitions == rhs.Repetitions;
        }

        public static bool operator !=(CubeConfiguration lhs, CubeConfiguration rhs){
            return !(lhs == rhs);
        }
    }
}
