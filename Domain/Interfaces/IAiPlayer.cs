using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAiPlayer : Iplayer
    {
        /// <summary>
        /// Kiest een zet gebaseerd op het huidige bord.
        /// </summary>
        /// <param name="board">Het huidige bord</param>
        /// <returns>Tuple (row, column)</returns>
        (int row, int col) GetMove(Board board);
    }
}
