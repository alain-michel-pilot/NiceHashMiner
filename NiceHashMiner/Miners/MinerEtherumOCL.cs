﻿using NiceHashMiner.Configs;
using NiceHashMiner.Devices;
using NiceHashMiner.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMiner.Miners {

    // TODO for NOW ONLY AMD
    // AMD or TODO it could be something else
    public class MinerEtherumOCL : MinerEtherum {

        private readonly int GPUPlatformNumber;

        public MinerEtherumOCL()
            : base(DeviceType.AMD, "MinerEtherumOCL", "AMD OpenCL") {
            GPUPlatformNumber = ComputeDeviceQueryManager.Instance.AMDOpenCLPlatformNum;
        }

        protected override string GetStartCommandStringPart(Algorithm miningAlgorithm, string url, string username) {
            // set directory
            WorkingDirectory = "";
            return " --opencl --opencl-platform " + GPUPlatformNumber
                + " " + miningAlgorithm.ExtraLaunchParameters
                + " -S " + url.Substring(14)
                + " -O " + username + ":" + Algorithm.PasswordDefault
                + " --api-port " + APIPort.ToString()
                + " --opencl-devices ";
        }

        protected override string GetBenchmarkCommandStringPart(ComputeDevice benchmarkDevice, Algorithm algorithm) {
            return " --opencl --opencl-platform " + GPUPlatformNumber
                + " " + algorithm.ExtraLaunchParameters
                + " --benchmark-warmup 40 --benchmark-trial 20"
                + " --opencl-devices ";
        }

    }
}
