﻿using System;

namespace MvcTemplate.Components.Security
{
    public interface IHasher
    {
        String Hash(String value);
        String HashPassword(String value);

        Boolean Verify(String value, String hash);
    }
}
