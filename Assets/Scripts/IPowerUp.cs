using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPowerUp
{
    IEnumerator ClaimPowerUp();
    void ResetPowerUp();
}
