        /// <summary>
        /// 優化腳本的位移命令,將多個位移命令轉成平滑的位移,缺點是會缺失位移過程的精準度
        /// </summary>
        /// <param name="cmds">命令來源</param>
        /// <param name="Distinction">允許的差距</param>
        /// <param name="CombineAbsolute">是否要整合絕對位移</param>
        /// <returns>共多少命令被整合</returns>
        public int MouseMoveOptimization(IList<ICommand> cmds, double Distinction, double SpeedDistinction)
        {
            int sum = 0;
            List<ICommand> Zipped = new List<ICommand>();   //存放所有完成壓縮的命令

            hMoveMouseAbs Ensure = null;                    //正在壓縮的命令物件實體
            double EnsureSpeed = Double.NaN;                //正在壓縮的命令的位移速度
            double EnsureSita = Double.NaN;                 //正在壓縮的命令的角度

            uint costTime = 0;                              //下一個位移命令前的等待時間

            for(int i = 0;i < cmds.Count; i++)
            {
                if (cmds[i] is Pause) 
                     costTime += (cmds[i] as Pause).PauseTime;
                else if(cmds[i] is MoveMouse)
                {
                    //一個完整的位移命令由costTime和MoveMouse組合而成,現在都有了
                    //開始判斷是否可以整合進Ensure(看游標移動速度和朝向角度)
                    MoveMouse cmd = (MoveMouse)cmds[i];
                    //Ensure為空判斷
                    if (Ensure == null)
                    {
                        Ensure = new hMoveMouseAbs(0, 0, cmd.mx, cmd.my, costTime);
                        EnsureSita = EnsureSpeed = Double.NaN;
                        costTime = 0;
                        continue;
                    }
                    //和上次操作的位移差距
                    int disx = cmd.mx - Ensure.sx - Ensure.mx;
                    int disy = cmd.my - Ensure.sy - Ensure.my;
                    //建立Sita和Speed
                    if (Double.IsNaN(EnsureSita) || Double.IsNaN(EnsureSpeed))
                    {
                        EnsureSita = GetAngle(cmd.mx - Ensure.sx, cmd.my - Ensure.sy);
                        EnsureSpeed = GetSpeed(cmd.mx - Ensure.sx, cmd.my - Ensure.sy, costTime);
                    }

                    //角度判斷
                    double newSita = GetAngle(disx,disy);
                    bool Success = true;
                    if (!InDifferenceRange(EnsureSita, newSita, Distinction))
                        Success = false;

                    //速度判斷
                    double newSpeed = GetSpeed(disx,disy,costTime);
                    if (!InSpeedDifferenceRange(EnsureSpeed, newSpeed, Distinction))
                        Success = false;

                    //確認這個位移能夠加入
                    if (Success)
                    {
                        Ensure.mx += disx;
                        Ensure.my += disy;
                        Ensure.time += costTime;
                        costTime = 0;
                        sum++;
                    }else
                    {
                        Zipped.Add(Ensure);
                        int sx = Ensure.sx + Ensure.mx;
                        int sy = Ensure.sy + Ensure.my;
                        Ensure = new hMoveMouseAbs(
                            cmd.mx - sx,
                            cmd.my - sy,
                            sx,
                            sy,
                            costTime);
                        EnsureSita = EnsureSpeed = Double.NaN;
                        costTime = 0;
                        continue;
                    }
                    
                }else
                {
                    //掃到的是和壓縮無關的命令,會中斷壓縮
                    if (costTime != 0)
                        Zipped.Add(new Pause(costTime));
                    costTime = 0;
                    if (Ensure != null)
                        Zipped.Add(Ensure);
                    Ensure = null;
                    Zipped.Add(cmds[i]);
                }
            }

            //結束壓縮,輸出所有命令
            if(costTime != 0)
                Zipped.Add(new Pause(costTime));
            costTime = 0;
            if (Ensure != null)
                Zipped.Add(Ensure);

            cmds.Clear();
            foreach (ICommand cmd in Zipped)
                cmds.Add(cmd);

            return sum;
        }
