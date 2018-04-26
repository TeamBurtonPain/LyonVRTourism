using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class ModelTest : MonoBehaviour
{
    public Text tquest;
    public Text tuser;

    public void Start()
    {
        Creator c = new Creator
        {
            Id = "0@bob",
            Username = "bob",
            Mail = "mail@frub.fufv",
            Password = "pass",
            LastName = "fdibs",
            FirstName = "bob",
            Xp = 0L,
            Role = RoleAccount.CREATOR
        };

        CheckPoint cp1 = new CheckPoint
        {
            Id = 1L,
            Picture = "/pic",
            Text = "Text of checkpoint",
            Choices = { "choix 1","choix2","choix3" },
            Answer = "choix1"
        };
        CheckPoint cp2 = new CheckPoint
        {
            Id = 2L,
            Picture = "/pic",
            Text = "Text of checkpoint",
            Choices = { "choix 1", "choix2", "choix3" },
            Answer = "choix2"
        };
        CheckPoint cp3 = new CheckPoint
        {
            Id = 3L,
            Picture = "/pic",
            Text = "Text of checkpoint",
            Choices = { "choix 1", "choix2", "choix3" },
            Answer = "choix3"
        };

        Quest q1 = new Quest
        {
            Title = "quete1",
            Description = "Super quete cool",
            Geolocalisation = {x = 10, y = 45},
            Open = true,
            Creator = c,
            Checkpoints = {cp1, cp2, cp3},
        };

        HTTPHelper.Send(c);

        tuser.text = JSONHelper.ToJsonString(q1);
    }


        
}

