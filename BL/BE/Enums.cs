using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum Gender { male, female }
    public enum CarTip { PrivateCarAutomatic, PrivateCarGear, motorcycleAutomatic, motorcycleGear,truckAutomatic,truckGear, HeavyTruckAutomatic, HeavyTruckGear }

    public enum TesterOptions {All_Testers,Type_car}
    public enum  TraineeOptions{ All_Trainees, Type_car,School_Name,Teacher_Car,Number_Tests,Is_Succedded_Tests }
    public enum TestOptions {All_Tests, Is_Valids,Is_Succededd,Need_to_update}
}
