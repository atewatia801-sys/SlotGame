\# Underpin Slot Machine



This is a 3-reel slot machine game made in Unity as part of the Unity Developer Intern assignment for Underpin Technology.



I focused mainly on making the basic slot machine mechanics work properly, while keeping the code organized and the gameplay smooth.



\## What I Built



\- 3 working reels with random results

\- Smooth reel spinning and staggered stopping

\- 4 different symbols: 7, Cherry, Bell and BAR

\- 3 matching symbols give a win

\- Different payout multipliers for each symbol

\- Starting balance and adjustable bet system

\- Minimum and maximum bet limits

\- Win popup showing the payout

\- Animated slot machine lever

\- Prevents spinning when the player doesn't have enough balance



\## Payouts



| Symbol | Payout |

|--------|--------|

| 7 - 7 - 7 | 10x bet |

| Cherry - Cherry - Cherry | 5x bet |

| Bell - Bell - Bell | 4x bet |

| BAR - BAR - BAR | 3x bet |



For example, if the bet is 50 and the result is `7 - 7 - 7`, the player receives 500.



\## Controls



\- \*\*SPIN\*\* – Start the slot machine

\- \*\*+\*\* – Increase the bet

\- \*\*−\*\* – Decrease the bet



The bet ranges from 10 to 200 and changes in steps of 10.



\## Project Structure



```text

Assets/

├── Animations/

├── Art/

├── Audio/

├── Prefabs/

├── Scenes/

├── Scripts/

└── UI/



Build/

└── WebGL/

