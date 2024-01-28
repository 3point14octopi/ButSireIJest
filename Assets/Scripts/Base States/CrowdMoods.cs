using System;
using UnityEngine;

namespace CharacterStates
{
    public interface ICrowdMood
    {
        void Init(CrowdState c);
        void OnJoke(JokeType joke);
        void OnUpdate();

        void Neutralize();
    }

    public class Crowd_GoodMood : ICrowdMood
    {
        CrowdState crowd;

        public void Init(CrowdState c)
        {
            crowd = c;
        }
        public void OnJoke(JokeType joke)
        {
            if(joke == JokeType.KING)
            {
                if (crowd.favour <= 13)
                {
                    crowd.favour += 2;
                }
                else
                {
                    crowd.favour = 15;
                }
                
            }else if(joke == JokeType.NEUTRAL)
            {
                crowd.favour -= 1;
                if(crowd.favour < 12) crowd.UpdateCrowdEmotion(MoodEnum.NEUTRAL);
            }
            else
            {
                crowd.favour -= 2;
                if (crowd.favour < 12) crowd.UpdateCrowdEmotion(MoodEnum.NEUTRAL);
            }
        }
        public void OnUpdate()
        {
            ScoreDump.AddToScore(2 * Time.deltaTime);
        }

        public void Neutralize()
        {
            if(crowd.favour > 14)
            {
                crowd.favour--;
            }else if(crowd.favour < 14)
            {
                crowd.favour++;
            }
        }
    }

    public class Crowd_NeutralMood : ICrowdMood
    {
        CrowdState crowd;

        public void Init(CrowdState c)
        {
            crowd = c;
        }
        public void OnJoke(JokeType joke)
        {
            if (joke == JokeType.KING)
            {
                crowd.favour += 2;
                if (crowd.favour > 12) crowd.UpdateCrowdEmotion(MoodEnum.HUMORED);

            }
            else if (joke == JokeType.NEUTRAL)
            {
                Neutralize();
            }
            else
            {
                crowd.favour -= 2;
                if (crowd.favour < 8) crowd.UpdateCrowdEmotion(MoodEnum.ANGRY);
            }
        }
        public void OnUpdate()
        {
            if (crowd.favour != 10) ScoreDump.AddToScore(Time.deltaTime);
        }

        public void Neutralize()
        {
            if (crowd.favour > 10)
            {
                crowd.favour--;
            }
            else if (crowd.favour < 10)
            {
                crowd.favour++;
            }
        }
    }

    public class Crowd_BadMood: ICrowdMood
    {
        CrowdState crowd;

        public void Init(CrowdState c)
        {
            crowd = c;
        }
        public void OnJoke(JokeType joke)
        {
            if (joke == JokeType.KING)
            {
                crowd.favour += 2;
                if (crowd.favour > 7) crowd.UpdateCrowdEmotion(MoodEnum.NEUTRAL);
            }
            else if (joke == JokeType.NEUTRAL)
            {
                if (crowd.favour > 0)
                {
                    crowd.favour --;
                }
                else
                {
                    crowd.favour = 0;
                }
            }
            else
            {
                if(crowd.favour >= 2)
                {
                    crowd.favour -= 2;
                }
                else
                {
                    crowd.favour = 0;
                }
            }
        }
        public void OnUpdate()
        {
            ScoreDump.AddToScore((8 - crowd.favour) * Time.deltaTime * 0.5f);

            //put tomato throwing logic here
        }

        public void Neutralize()
        {
            if(crowd.favour > 0)
            {
                if (crowd.favour > 4)
                {
                    crowd.favour--;
                }
                else if (crowd.favour < 4)
                {
                    crowd.favour++;
                }
            }
            
        }

        private void ThrowTomatoes()
        {

        }
    }
    
}
