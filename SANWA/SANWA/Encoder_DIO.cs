using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Modbus;

namespace SANWA.Utility
{
    public class EncoderDIO
    {
        public class ICPcon
        {
            private ushort DigitalInputQuantity;
            private Modbus.Device.ModbusIpMaster Master;

            public ICPcon(ushort digitalInputQuantity, Modbus.Device.ModbusIpMaster master)
            {
                try
                {
                    DigitalInputQuantity = digitalInputQuantity;
                    Master = master;
                    Master.Transport.Retries = 0;
                    Master.Transport.ReadTimeout = 1500;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }

            public bool[] ReadInputs(byte slaveID, ushort coilAddress)
            {
                return Master.ReadInputs(slaveID, coilAddress, DigitalInputQuantity);
            }

            public bool[] ReadCoils(byte slaveID, ushort coilAddress)
            {
                return Master.ReadCoils(slaveID, coilAddress, DigitalInputQuantity);
            }

            //public ushort[] ReadInputRegisters(byte slaveID, ushort coilAddress)
            //{

            //    ushort[] ussReadRegisters = Master.ReadInputRegisters(slaveID, coilAddress, DigitalInputQuantity);
            //    ushort[] ussReadConvert = new ushort[(int)DigitalInputQuantity];


            //    for (int i = 0; i <= ussReadRegisters.Length; i++)
            //    {
            //        if (ussReadRegisters[0] != 0)
            //        {
            //            double value = double.Parse((((int)ussReadRegisters[i] / 4096) + 4) / 1000);
            //            ussReadConvert(i) = value.ToString("0.000");
            //        }
            //    }


            //    return 
            //}

            public ushort[] ReadHoldingRegisters(byte slaveID, ushort coilAddress)
            {
                return Master.ReadHoldingRegisters(slaveID, coilAddress, DigitalInputQuantity);
            }


            public void WriteSingleCoil(byte slaveID, ushort coilAddress, bool OnOff)
            {
                Master.WriteSingleCoil(slaveID, coilAddress, OnOff);
            }

            public void WriteMultipleCoil(byte slaveID, ushort coilAddress, bool[] OnOffList)
            {
                Master.WriteMultipleCoils(slaveID, coilAddress, OnOffList);
            }


            //public void WriteSingleCoil(byte slaveID, ushort coilAddress, bool OnOff)
            //{
            //    Master.WriteSingleRegister()
            //}

            //public void WriteMultipleCoil(byte slaveID, ushort coilAddress, bool[] OnOffList)
            //{
            //    Master.WriteMultipleCoils(slaveID, coilAddress, OnOffList);
            //}
        }
    }
}
