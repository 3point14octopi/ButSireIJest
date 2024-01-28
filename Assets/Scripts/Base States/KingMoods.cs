using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CharacterStates
{
    public interface IKingMood
    {
        void Init(KingState k);
        void OnJoke(JokeType joke);
        void OnUpdate();
        void SetSprite();

        void BoredomNeutralizer();

    }

    public class King_GoodMood : IKingMood
    {
        KingState kStateObj;

        public void Init(KingState script)
        {
            kStateObj = script;
        }


        public void OnUpdate()
        {
            ScoreDump.AddToScore(((kStateObj.kingApproval == 15) ? 3 : 2) * Time.deltaTime);
        }

        public void OnJoke(JokeType j)
        {
            if(j == JokeType.KING)
            {
                kStateObj.kingApproval -= 2;
                if (kStateObj.kingApproval < 11)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.NEUTRAL);
                }
               
            }else if(j == JokeType.NEUTRAL)
            {
                kStateObj.kingApproval--;
                
                if(kStateObj.kingApproval < 11)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.NEUTRAL);
                }
            }
            else
            {
                kStateObj.kingApproval += (kStateObj.kingApproval > 14) ? 0 : 1;
            }
        }

        public void SetSprite()
        {
            kStateObj.UpdateMoodSprite(kStateObj.expressions[(int)MoodEnum.HUMORED]);
        }

        public void BoredomNeutralizer()
        {
            if(kStateObj.kingApproval > 13)
            {
                kStateObj.kingApproval--;
            }else if(kStateObj.kingApproval < 13)
            {
                kStateObj.kingApproval++;
            }
        }

    }

    public class King_NeutralMood : IKingMood
    {
        KingState kStateObj;

        public void Init(KingState script)
        {
            kStateObj = script;
        }


        public void OnUpdate()
        {
            if(kStateObj.kingApproval != 8) ScoreDump.AddToScore(Time.deltaTime);
        }

        public void OnJoke(JokeType j)
        {
            if (j == JokeType.KING)
            {
                kStateObj.kingApproval -= 3;
                if (kStateObj.kingApproval < 6)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.ANGRY);
                }

            }
            else if (j == JokeType.NEUTRAL)
            {
                if(kStateObj.kingApproval > 8)
                {
                    kStateObj.kingApproval--; 
                }else if (kStateObj.kingApproval < 8)
                {
                    kStateObj.kingApproval++;
                }
            }
            else
            {
                kStateObj.kingApproval++;
                if(kStateObj.kingApproval > 10)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.HUMORED);
                }
            }
        }

        public void SetSprite()
        {
            kStateObj.UpdateMoodSprite(kStateObj.expressions[(int)MoodEnum.NEUTRAL]);
        }

        public void BoredomNeutralizer()
        {
            if (kStateObj.kingApproval > 8)
            {
                kStateObj.kingApproval--;
            }
            else if (kStateObj.kingApproval < 8)
            {
                kStateObj.kingApproval++;
            }
        }
    }

    public class King_AngryMood : IKingMood
    {
        KingState kStateObj;

        public void Init(KingState script)
        {
            kStateObj = script;
        }


        public void OnUpdate()
        {
            
            ScoreDump.AddToScore(((kStateObj.kingApproval > 3) ? 2 : 3) * Time.deltaTime);
        }

        public void OnJoke(JokeType j)
        {
            if (j == JokeType.KING)
            {
                kStateObj.kingApproval -= 4;
                if (kStateObj.kingApproval < 1)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.NUCLEAR);
                }

            }
            else if (j == JokeType.NEUTRAL)
            {
                kStateObj.kingApproval += 1;
                if(kStateObj.kingApproval > 5)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.NEUTRAL);
                }
            }
            else
            {
                kStateObj.kingApproval++;
                if(kStateObj.kingApproval < 5)
                {
                    kStateObj.UpdateEmotionalState(MoodEnum.NEUTRAL);
                }
            }
        }

        public void SetSprite()
        {
            kStateObj.UpdateMoodSprite(kStateObj.expressions[(int)MoodEnum.ANGRY]);
        }

        public void BoredomNeutralizer()
        {
            if (kStateObj.kingApproval > 3)
            {
                kStateObj.kingApproval--;
            }
            else if (kStateObj.kingApproval < 3)
            {
                kStateObj.kingApproval++;
            }
        }
    }

    public class King_NuclearMood : IKingMood
    {
        KingState kStateObj;
        float timeInState = 0f;
        bool prolongedSent = false;

        public void Init(KingState script)
        {
            kStateObj = script;
        }


        public void OnUpdate()
        {
            if(timeInState > 2f)
            {
                SceneManager.LoadScene("gameOver");
            }
            else if(timeInState > 1f && !prolongedSent)
            {
                kStateObj.InProlongedRageState();
                prolongedSent = true;
            }
            
            ScoreDump.AddToScore(5 * Time.deltaTime);
            timeInState += Time.deltaTime;
        }

        public void OnJoke(JokeType j)
        {
            if (j == JokeType.CROWD)
            {
                kStateObj.kingApproval = 1;
                kStateObj.UpdateEmotionalState(MoodEnum.ANGRY);
            }
            else
            {
                SceneManager.LoadScene("gameOver");
            }
        }

        public void SetSprite()
        {
            timeInState = 0f;
            prolongedSent = false;
            kStateObj.UpdateMoodSprite(kStateObj.expressions[(int)MoodEnum.NUCLEAR]);
        }

        public void BoredomNeutralizer()
        {
            //nothing happens. he is SO mad. 
        }
    }
}
