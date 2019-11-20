namespace GameBase
{
    public class BaseConst 
    {
        #region UI
        public static readonly int UI_MAX_DEPTH;
        public static readonly int UI_WINDOW_STACK_MAX_DEPTH;
        public static readonly int UI_SORTING_ORDER_SPACE;
        #endregion


        #region TimerManager
        public static readonly int TIMER_MAX_TASK_PER_FRAME;
        #endregion

        static BaseConst()
        {
            UI_MAX_DEPTH = 31;
            UI_WINDOW_STACK_MAX_DEPTH = 31;
            UI_SORTING_ORDER_SPACE = 100;
            TIMER_MAX_TASK_PER_FRAME = 64;
        }
    }

}
