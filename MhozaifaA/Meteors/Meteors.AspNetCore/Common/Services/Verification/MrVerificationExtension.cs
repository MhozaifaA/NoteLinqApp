﻿using Meteors.AspNetCore.Common.AuxiliaryImplemental.Classes;
using Meteors.AspNetCore.Common.Secure;
using Meteors.AspNetCore.Common.Services.Verification.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.Verification
{
    public class MrVerificationExtension : SecureHasher
    {
        public static string Generate(MrVerificationOptions option, out DateTime expire, out string hash)
        {
            if (option is null)
                throw new ArgumentNullException($"{nameof(MrVerification)} {nameof(option)} can't be null");

            if (option.Size <= 0)
                throw new ArgumentException($"{nameof(MrVerification)} {nameof(option.Size)} can't be 0 or low");

            if (option.Length <= 0)
                throw new ArgumentException($"{nameof(MrVerification)} {nameof(option.Length)} can't be 0 or low");

            if (option.Expire < 0)
                throw new ArgumentException($"{nameof(MrVerification)} {nameof(option.Expire)} can't be low than 0");

            if (option.Iterations <= 0)
                throw new ArgumentException($"{nameof(MrVerification)} {nameof(option.Iterations)} can't be 0 or low");

            DateTime dateNow = DateTime.Now;
            string plain = Generator.RandomString(option.Size,StringsOfLetters.Number);
            expire = dateNow.AddSeconds(59 - dateNow.Second).AddMinutes(option.Expire - 1);
            hash = Hash(plain + dateNow.ToString("yyyyMMddHHmm"), option.Length, option.Iterations);
            return plain;
        }

        public static string Generate(out DateTime expire, out string hash)
        {
            return Generate(new MrVerificationOptions(), out expire, out hash);
        }

        public static string Generate(MrVerificationOptions option, out string hash)
        {
            return Generate(option, out _, out hash);
        }

        public static string Generate(out string hash)
        {
            return Generate(new MrVerificationOptions(), out hash);
        }

        public static string Generate(MrVerificationOptions option, out DateTime expire)
        {
            return Generate(option, out expire, out _);
        }

        public static string Generate(out DateTime expire)
        {
            return Generate(new MrVerificationOptions(), out expire);
        }

        public static string Generate(MrVerificationOptions option)
        {
            return Generate(option, out _, out _);
        }

        public static string Generate()
        {
            return Generate(new MrVerificationOptions());
        }


        public static bool Scan(string plain, string hash, MrVerificationOptions option)
        {
            if (string.IsNullOrEmpty(plain))
                throw new ArgumentNullException($"{nameof(MrVerification)} {nameof(plain)} can't be null or empty");

            if (string.IsNullOrEmpty(hash))
                throw new ArgumentNullException($"{nameof(MrVerification)} {nameof(hash)} can't be null or empty");

            bool verify;
            int begin = 0;
            do
            {
                verify = Verify(plain + DateTime.Now.AddMinutes(-begin).ToString("yyyyMMddHHmm"), hash);
                begin++;
            } while (verify == false && begin <= option.Expire);

            return verify;
        }

        public static bool Scan(string plain, string hash, int expire)
        {
            return Scan(plain, hash, new MrVerificationOptions() { Expire = expire });
        }

        public static bool Scan(string plain, string hash)
        {
            return Scan(plain, hash, new MrVerificationOptions());
        }

    }
}
