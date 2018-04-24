using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Controller
{
    public enum ConnexionState
    {
        CONNEXION_LOCAL = 0,
        CONNEXION_SERVER,
        DISCONNECTED
    }

    public class Controller
    {
        private State currentState;
        private ConnexionState currentConnexion;

        /// <summary>
        /// Transitions to the specified state s
        /// </summary>
        /// <param name="s">The state.</param>
        //TODO : voir quoi faire d'autre pour effectuer la transition 
        public void Transition(State s)
        {
            currentState = s;
        }


        /*********** BOUTONS ***********/

        public void loginLocal()
        {

        }

        /*********** FIN BOUTONS ***********/
    }
}