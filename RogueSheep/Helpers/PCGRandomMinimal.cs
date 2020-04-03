namespace RogueSheep.Helpers
{
    /*
    * PCG Random Number Generation for C#
    * 
    * Original algorithm for C/C++ (c) by Melissa O'Neill <oneill@pcg-random.org>
    * and PCG Project contributors.
    * 
    * Original licensed under Apache 2.0 or MIT license.
    * 
    * For additional information about the PCG random number generation scheme,
    * original source code and binary builds, as well as original license details
    * visit http://www.pcg-random.org/.
    * 
    */
    public class PCGRandomMinimal
    {
        private ulong state;
        private readonly ulong stream;

        public PCGRandomMinimal(ulong initState, ulong initStream)
        {
            state = 0;
            stream = initStream << 1 | 1;
            Step();
            state += initState;
            Step();
        }

        public uint Step()
        {
            ulong oldState = state;
            state = (oldState * 6364136223846793005) + stream;
            uint xorshifted = (uint)(((oldState >> 18) ^ oldState) >> 27);
            uint rot = (uint)(oldState >> 59);
            return (xorshifted >> (int)rot) | (xorshifted << ((int)-rot & 31));
        }
    }
}
