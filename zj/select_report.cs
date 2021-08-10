using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace zj
{
    class select_report
    {
        public DataTable dt;
        //银行账户情况汇总统计表（按账户类别）
        public DataTable report_A4_1(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //获得单位名称
            String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.zhlb,count(t_yhzh.id) as sl,sum(t_yhzh.ckye) as ye from t_dwxx,t_yhzh where left(t_yhzh.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.zhlb order by t_dwxx.dwdm";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 17; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds_1.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 17; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                dt.Rows[i][1] = ds_1.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "财务单一账户")
                    {
                        dt.Rows[i][4] = ds.Tables[0].Rows[k]["sl"].ToString();
                        dt.Rows[i][5] = ds.Tables[0].Rows[k]["ye"].ToString();
                        continue;
                    }
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "小额账户")
                    {
                        dt.Rows[i][6] = ds.Tables[0].Rows[k]["sl"].ToString();
                        dt.Rows[i][7] = ds.Tables[0].Rows[k]["ye"].ToString();
                        continue;
                    }
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "零余额账户")
                    {
                        dt.Rows[i][8] = ds.Tables[0].Rows[k]["sl"].ToString();
                        continue;
                    }
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "一般存款账户")
                    {
                        dt.Rows[i][9] = ds.Tables[0].Rows[k]["sl"].ToString();
                        dt.Rows[i][10] = ds.Tables[0].Rows[k]["ye"].ToString();
                        continue;
                    }
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "专用存款账户")
                    {
                        dt.Rows[i][11] = ds.Tables[0].Rows[k]["sl"].ToString();
                        dt.Rows[i][12] = ds.Tables[0].Rows[k]["ye"].ToString();
                        continue;
                    }
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "临时存款账户")
                    {
                        dt.Rows[i][13] = ds.Tables[0].Rows[k]["sl"].ToString();
                        dt.Rows[i][14] = ds.Tables[0].Rows[k]["ye"].ToString();
                        continue;
                    }
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString() && ds.Tables[0].Rows[k]["zhlb"].ToString() == "POS转账卡账户")
                    {
                        dt.Rows[i][15] = ds.Tables[0].Rows[k]["sl"].ToString();
                        dt.Rows[i][16] = ds.Tables[0].Rows[k]["ye"].ToString();
                        continue;
                    }

                }
                dt.Rows[i][2] = (int.Parse(dt.Rows[i][4].ToString()) + int.Parse(dt.Rows[i][6].ToString()) + int.Parse(dt.Rows[i][8].ToString()) + int.Parse(dt.Rows[i][9].ToString()) + int.Parse(dt.Rows[i][11].ToString()) + int.Parse(dt.Rows[i][13].ToString()) + int.Parse(dt.Rows[i][15].ToString())).ToString();
                dt.Rows[i][3] = (double.Parse(dt.Rows[i][5].ToString()) + double.Parse(dt.Rows[i][7].ToString()) + double.Parse(dt.Rows[i][10].ToString()) + double.Parse(dt.Rows[i][12].ToString()) + double.Parse(dt.Rows[i][14].ToString()) + double.Parse(dt.Rows[i][16].ToString())).ToString();
            }
            return dt;
        }
        //银行账户情况汇总统计表（按银行类别）
        public DataTable report_A4_2(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //获得单位名称
            String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.hb,count(t_yhzh.id) as sl,sum(t_yhzh.ckye) as ye from t_dwxx,t_yhzh where left(t_yhzh.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.hb order by t_dwxx.dwdm";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 16; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds_1.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 16; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();
                dt.Rows[i][1] = ds_1.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString())
                    {
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "工商银行")
                        {
                            dt.Rows[i][4] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][5] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "农业银行")
                        {
                            dt.Rows[i][6] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][7] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "中国银行")
                        {
                            dt.Rows[i][8] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][9] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "建设银行")
                        {
                            dt.Rows[i][10] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][11] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }//交通银行
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "交通银行")
                        {
                            dt.Rows[i][12] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][13] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        //其他银行（不属于上述情况时，全部其他银行）
                        dt.Rows[i][14] = int.Parse(dt.Rows[i][14].ToString()) + int.Parse(ds.Tables[0].Rows[k]["sl"].ToString());
                        dt.Rows[i][15] = double.Parse(dt.Rows[i][15].ToString()) + double.Parse(ds.Tables[0].Rows[k]["ye"].ToString());
                    }

                }
                dt.Rows[i][2] = (int.Parse(dt.Rows[i][4].ToString()) + int.Parse(dt.Rows[i][6].ToString()) + int.Parse(dt.Rows[i][8].ToString()) + int.Parse(dt.Rows[i][10].ToString()) + int.Parse(dt.Rows[i][12].ToString()) + int.Parse(dt.Rows[i][14].ToString())).ToString();
                dt.Rows[i][3] = (double.Parse(dt.Rows[i][5].ToString()) + double.Parse(dt.Rows[i][7].ToString()) + double.Parse(dt.Rows[i][9].ToString()) + double.Parse(dt.Rows[i][11].ToString()) + double.Parse(dt.Rows[i][13].ToString()) + double.Parse(dt.Rows[i][15].ToString())).ToString();
            }
            return dt;
        }
        //定期(通知)存款情况汇总统计表
        public DataTable report_A4_3(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //获得单位名称
            String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_dqck.sfkhh,t_dqck.hb,count(t_dqck.id) as sl,sum(t_dqck.je) as ye from t_dwxx,t_dqck where left(t_dqck.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_dqck.sfkhh,t_dqck.hb order by t_dwxx.dwdm";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 18; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds_1.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 18; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();
                dt.Rows[i][1] = ds_1.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString())
                    {
                        //如果在本单位开户行存储，直接将数据累计
                        if (ds.Tables[0].Rows[k]["sfkhh"].ToString() == "是")
                        {
                            dt.Rows[i][4] = int.Parse(dt.Rows[i][4].ToString()) + int.Parse(ds.Tables[0].Rows[k]["sl"].ToString());
                            dt.Rows[i][5] = double.Parse(dt.Rows[i][5].ToString()) + double.Parse(ds.Tables[0].Rows[k]["ye"].ToString());
                            continue;
                        }
                        else
                        {
                            //如果不在本单位开户行存储，分银行进行统计
                            if (ds.Tables[0].Rows[k]["hb"].ToString() == "工商银行")
                            {
                                dt.Rows[i][6] = ds.Tables[0].Rows[k]["sl"].ToString();
                                dt.Rows[i][7] = ds.Tables[0].Rows[k]["ye"].ToString();
                                continue;
                            }
                            if (ds.Tables[0].Rows[k]["hb"].ToString() == "农业银行")
                            {
                                dt.Rows[i][8] = ds.Tables[0].Rows[k]["sl"].ToString();
                                dt.Rows[i][9] = ds.Tables[0].Rows[k]["ye"].ToString();
                                continue;
                            }
                            if (ds.Tables[0].Rows[k]["hb"].ToString() == "中国银行")
                            {
                                dt.Rows[i][10] = ds.Tables[0].Rows[k]["sl"].ToString();
                                dt.Rows[i][11] = ds.Tables[0].Rows[k]["ye"].ToString();
                                continue;
                            }
                            if (ds.Tables[0].Rows[k]["hb"].ToString() == "建设银行")
                            {
                                dt.Rows[i][12] = ds.Tables[0].Rows[k]["sl"].ToString();
                                dt.Rows[i][13] = ds.Tables[0].Rows[k]["ye"].ToString();
                                continue;
                            }
                            //其他银行的情况
                            if (ds.Tables[0].Rows[k]["hb"].ToString() == "其他银行" || ds.Tables[0].Rows[k]["hb"].ToString() == "邮政储蓄银行" || ds.Tables[0].Rows[k]["hb"].ToString() == "农业发展银行" || ds.Tables[0].Rows[k]["hb"].ToString() == "农村商业银行" || ds.Tables[0].Rows[k]["hb"].ToString() == "城市商业银行" || ds.Tables[0].Rows[k]["hb"].ToString() == "农村信用社")
                            {
                                dt.Rows[i][16] = int.Parse(dt.Rows[i][16].ToString()) + int.Parse(ds.Tables[0].Rows[k]["sl"].ToString());
                                dt.Rows[i][17] = double.Parse(dt.Rows[i][17].ToString()) + double.Parse(ds.Tables[0].Rows[k]["ye"].ToString());
                                continue;
                            }
                            //剩下是股份制银行（不属于上述情况时，全部纳入股份制银行）
                            dt.Rows[i][14] = int.Parse(dt.Rows[i][14].ToString()) + int.Parse(ds.Tables[0].Rows[k]["sl"].ToString());
                            dt.Rows[i][15] = double.Parse(dt.Rows[i][15].ToString()) + double.Parse(ds.Tables[0].Rows[k]["ye"].ToString());
                        }
                    }
                }
                dt.Rows[i][2] = (int.Parse(dt.Rows[i][4].ToString()) + int.Parse(dt.Rows[i][6].ToString()) + int.Parse(dt.Rows[i][8].ToString()) + int.Parse(dt.Rows[i][10].ToString()) + int.Parse(dt.Rows[i][12].ToString()) + int.Parse(dt.Rows[i][14].ToString()) + int.Parse(dt.Rows[i][16].ToString())).ToString();
                dt.Rows[i][3] = (double.Parse(dt.Rows[i][5].ToString()) + double.Parse(dt.Rows[i][7].ToString()) + double.Parse(dt.Rows[i][9].ToString()) + double.Parse(dt.Rows[i][11].ToString()) + double.Parse(dt.Rows[i][13].ToString()) + double.Parse(dt.Rows[i][15].ToString()) + double.Parse(dt.Rows[i][17].ToString())).ToString();
            }
            return dt;
        }
        //资金结存
        public DataTable report_A4_4(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //统计相关信息
            // String sql_select_zjjc = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(t_zjjc.kcxj)+sum(t_zjjc.hqck)+sum(t_zjjc.dqck)+sum(t_zjjc.yjzq) as zjjchj,sum(kcxj),sum(hqck),sum(dqck),sum(yjzq),sum(ysjf)+sum(dnyswjf)+sum(lnjfjy)+sum(brzxzj)-sum(bczxzj)+sum(zczxzj)+sum(ysjjf)+sum(zyjj)+sum(brwcxjf)+sum(brjsxjf)+sum(brzfzxjf)+sum(dtf)+sum(lydtf)+sum(zsk),sum(ysjf),sum(dnyswjf),sum(lnjfjy),sum(brzxzj)-sum(bczxzj)+sum(zczxzj),sum(ysjjf),sum(zyjj),sum(brwcxjf)+sum(brjsxjf)+sum(brzfzxjf),sum(dtf)+sum(lydtf),sum(zsk) from t_dwxx,t_zjjc where left(t_zjjc.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            String sql_select_zjjc = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(t_zjjc.kcxj)+sum(t_zjjc.hqck)+sum(t_zjjc.dqck)+sum(t_zjjc.yjzq) as zjjchj,sum(kcxj),sum(hqck),sum(dqck),sum(yjzq),sum(ysjf)+sum(dnyswjf)+sum(lnjfjy)+sum(brzxzj)-sum(bczxzj)+sum(zczxzj)+sum(ysjjf)+sum(zyjj)+sum(swgyjj)+sum(zsk)+sum(dtf)+sum(lydtf)-sum(kcwzye)-sum(zfk),sum(ysjf),sum(dnyswjf),sum(lnjfjy),sum(brzxzj)-sum(bczxzj)+sum(zczxzj),sum(ysjjf),sum(zyjj),sum(swgyjj)+sum(zsk)+sum(dtf)+sum(lydtf)-sum(kcwzye)-sum(zfk) from t_dwxx,t_zjjc where left(t_zjjc.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_zjjc);
            for (int m = 0; m < 15; m++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j < 15; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
            }
            return dt;
        }
        //现金使用
        public DataTable report_A4_5(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();
            //统计相关信息
            String sql_select_xjsy = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(rydyzc)+sum(zfgtjyzc)+sum(clfzc)+sum(yxzc)+sum(lxzc)+sum(bskzc)+sum(qtzc),sum(rydyzc),sum(zfgtjyzc),sum(clfzc),sum(yxzc),sum(lxzc),sum(bskzc),sum(qtzc) from t_dwxx,t_xjsy where left(t_xjsy.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_xjsy);

            for (int m = 0; m < 11; m++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j < 10; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
            }

            return dt;
        }
        //银行账户清查处理
        public DataTable report_A4_7(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //统计相关信息
            String sql_select_yhzhqccl = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(t_yhzhqccl.szkszh),sum(t_yhzhqccl.yqzh), sum(t_yhzhqccl.ycwczh), sum(t_yhzhqccl.qtwgzh), sum(t_yhzhqccl.wgccbs), sum(t_yhzhqccl.wgccje), sum(t_yhzhqccl.wgjdbs), sum(t_yhzhqccl.wgjdje), sum(t_yhzhqccl.qtyybs), sum(t_yhzhqccl.qtyyje), sum(t_yhzhqccl.gfsxbs), sum(t_yhzhqccl.gfsxje) from t_dwxx,t_yhzhqccl where left(t_yhzhqccl.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhqccl);

            for (int m = 0; m < 14; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j < 14; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
            }
            return dt;
        }
        //A3型报表——银行账户表
        public DataTable report_A3_1(String reportname)
        {
            //将A4纸报表中表二部分加入到表一的dt中
            DataTable dt_all = new DataTable();
            DataTable dt_1 = new DataTable();//按账户类别取
            DataTable dt_2 = new DataTable();//按开户行取
            dt_1 = report_A4_1("");
            dt_2 = report_A4_2("");
            //for(int m=0;m<33;m++)
            for (int m = 0; m < 28; m++)
            {
                dt_all.Columns.Add();
            }
            for (int i = 0; i < dt_1.Rows.Count; i++)
            {
                dt_all.Rows.Add();
                dt_all.Rows[i][0] = dt_1.Rows[i][0].ToString();
                dt_all.Rows[i][1] = dt_1.Rows[i][1].ToString();
                dt_all.Rows[i][2] = dt_1.Rows[i][2].ToString();
                dt_all.Rows[i][3] = dt_1.Rows[i][3].ToString();
                dt_all.Rows[i][4] = int.Parse(dt_1.Rows[i][4].ToString()) + int.Parse(dt_1.Rows[i][6].ToString()) + int.Parse(dt_1.Rows[i][8].ToString());
                dt_all.Rows[i][5] = double.Parse(dt_1.Rows[i][5].ToString()) + double.Parse(dt_1.Rows[i][7].ToString());
                //for(int m=6;m<19;m++)
                for (int m = 6; m < 12; m++)
                {
                    //dt_all.Rows[i][m] = dt_1.Rows[i][m - 2];
                    dt_all.Rows[i][m] = dt_1.Rows[i][m + 3];
                }
                //for (int j = 19; j < 33; j++)
                for (int j = 12; j < 26; j++)
                {
                    //dt_all.Rows[i][j] = dt_2.Rows[i][j-17];
                    dt_all.Rows[i][j] = dt_2.Rows[i][j - 10];
                }
                for (int x = 26; x < 28; x++)
                {
                    dt_all.Rows[i][x] = dt_1.Rows[i][x - 11];
                }
            }
            return dt_all;
        }
        //A3定期通知存款情况汇总表
        public DataTable report_A3_2(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //获得单位名称
            String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_dqck.hb,count(t_dqck.id) as sl,sum(t_dqck.je) as ye from t_dwxx,t_dqck where left(t_dqck.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_dqck.hb order by t_dwxx.dwdm";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 20; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds_1.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 20; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();
                dt.Rows[i][1] = ds_1.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    if (ds.Tables[0].Rows[k]["dwmc"].ToString() == dt.Rows[i][1].ToString())
                    {
                        //如果不在本单位开户行存储，分银行进行统计
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "工商银行")
                        {
                            dt.Rows[i][4] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][5] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "农业银行")
                        {
                            dt.Rows[i][6] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][7] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "中国银行")
                        {
                            dt.Rows[i][8] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][9] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "建设银行")
                        {
                            dt.Rows[i][10] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][11] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "交通银行")
                        {
                            dt.Rows[i][12] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][13] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "光大银行")
                        {
                            dt.Rows[i][14] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][15] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        if (ds.Tables[0].Rows[k]["hb"].ToString() == "中信银行")
                        {
                            dt.Rows[i][16] = ds.Tables[0].Rows[k]["sl"].ToString();
                            dt.Rows[i][17] = ds.Tables[0].Rows[k]["ye"].ToString();
                            continue;
                        }
                        //剩下是其他银行（不属于上述情况时，全部其他银行）
                        dt.Rows[i][18] = int.Parse(dt.Rows[i][18].ToString()) + int.Parse(ds.Tables[0].Rows[k]["sl"].ToString());
                        dt.Rows[i][19] = double.Parse(dt.Rows[i][19].ToString()) + double.Parse(ds.Tables[0].Rows[k]["ye"].ToString());
                    }
                }
                //所有合计
                dt.Rows[i][2] = int.Parse(dt.Rows[i][4].ToString()) + int.Parse(dt.Rows[i][6].ToString()) + int.Parse(dt.Rows[i][8].ToString()) + int.Parse(dt.Rows[i][10].ToString()) + int.Parse(dt.Rows[i][12].ToString()) + int.Parse(dt.Rows[i][14].ToString()) + int.Parse(dt.Rows[i][16].ToString()) + int.Parse(dt.Rows[i][18].ToString());
                dt.Rows[i][3] = double.Parse(dt.Rows[i][5].ToString()) + double.Parse(dt.Rows[i][7].ToString()) + double.Parse(dt.Rows[i][9].ToString()) + double.Parse(dt.Rows[i][11].ToString()) + double.Parse(dt.Rows[i][13].ToString()) + double.Parse(dt.Rows[i][15].ToString()) + double.Parse(dt.Rows[i][17].ToString()) + double.Parse(dt.Rows[i][19].ToString());
            }
            return dt;
        }
        //A3资金结存情况统计表
        public DataTable report_A3_3(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //统计相关信息
            String sql_select_zjjc = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(kcxj)+sum(hqck)+sum(dqck)+sum(yjzq) as zjjchj,sum(kcxj),sum(hqck),sum(dqck),sum(yjzq),sum(ysjf)+sum(dnyswjf)+sum(lnjfjy)+sum(zczxzj)+sum(ysjjf)+sum(zyjj)+sum(dtf)+sum(zsk),sum(ysjf),sum(dnyswjf),sum(lnjfjy),sum(zczxzj),sum(ysjjf),sum(zyjj),sum(brwcxjf) + sum(brjsxjf) + sum(brzfzxjf),sum(dtf),sum(zsk),sum(kcwzye) + sum(qt), sum(kcwzye), sum(bcwcxjf) + sum(bcjsxjf) + sum(bczfzxjf), sum(zfk),sum(qt) from t_dwxx, t_zjjc  where left(t_zjjc.dwdm,3)= t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            //String sql_select_zjjc = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(t_zjjc.kcxj)+sum(t_zjjc.hqck)+sum(t_zjjc.dqck)+sum(t_zjjc.yjzq) as zjjchj,sum(kcxj),sum(hqck),sum(dqck),sum(yjzq),sum(ysjf)+sum(dnyswjf)+sum(lnjfjy)+sum(brzxzj)-sum(bczxzj)+sum(zczxzj)+sum(ysjjf)+sum(zyjj)+sum(swgyjj)+sum(zsk)+sum(dtf)+sum(lydtf)-sum(kcwzye)-sum(zfk),sum(ysjf),sum(dnyswjf),sum(lnjfjy),sum(brzxzj)-sum(bczxzj)+sum(zczxzj),sum(ysjjf),sum(zyjj),sum(swgyjj)+sum(zsk)+sum(dtf)+sum(lydtf)-sum(kcwzye)-sum(zfk) from t_dwxx,t_zjjc where left(t_zjjc.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_zjjc);
            for (int m = 0; m < 22; m++)
            {
                dt.Columns.Add();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i][0] = (i + 1).ToString();
                    for (int j = 1; j < 22; j++)
                    {




                        if (j < 14)
                        {
                            dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                        }
                        else if (j == 14)
                        {
                            string sql_sjbk = "SELECT sum(brzcjf) as a,sum(brzfzxjf) as b,sum(brzxzj) as c,sum(bczcjf) as d,sum(bczfzxjf) as e,sum(bczxzj) as f FROM t_wlkx where dwdm='" + ds.Tables[0].Rows[i][0] + "'; ";
                            DataSet dt_sjk = AccessHelper.getDataSet(sql_sjbk);
                            double value = 0;
                            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["a"].ToString()))
                            {
                                value += double.Parse(dt_sjk.Tables[0].Rows[0]["a"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["b"].ToString()))
                            {
                                value += double.Parse(dt_sjk.Tables[0].Rows[0]["b"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["c"].ToString()))
                            {
                                value += double.Parse(dt_sjk.Tables[0].Rows[0]["c"].ToString());
                            }
                            //dt.Rows[i][j] = (double.Parse(dt_sjk.Tables[0].Rows[0]["a"].ToString()) + double.Parse(dt_sjk.Tables[0].Rows[0]["b"].ToString()) + double.Parse(dt_sjk.Tables[0].Rows[0]["c"].ToString())).ToString();

                            dt.Rows[i][j] = value;
                        }
                        else if (14 < j && j < 19)
                        {
                            dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                        }
                        else if (j == 19)
                        {
                            string sql_sjbk = "SELECT sum(brzcjf) as a,sum(brzfzxjf) as b,sum(brzxzj) as c,sum(bczcjf) as d,sum(bczfzxjf) as e,sum(bczxzj) as f FROM t_wlkx where dwdm='" + ds.Tables[0].Rows[i][0] + "'; ";
                            DataSet dt_sjk = AccessHelper.getDataSet(sql_sjbk);
                            double value = 0;
                            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["d"].ToString()))
                            {
                                value += double.Parse(dt_sjk.Tables[0].Rows[0]["d"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["e"].ToString()))
                            {
                                value += double.Parse(dt_sjk.Tables[0].Rows[0]["e"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["f"].ToString()))
                            {
                                value += double.Parse(dt_sjk.Tables[0].Rows[0]["f"].ToString());
                            }
                            dt.Rows[i][j] = value;
                        }
                        else if (j == 20)
                        {
                            string sql_jdk = "  SELECT sum(je) as a FROM t_jdk   where dwdm='" + ds.Tables[0].Rows[i][0] + "'; ";
                            DataSet dt_jdk = AccessHelper.getDataSet(sql_jdk);
                            if (!string.IsNullOrEmpty(dt_jdk.Tables[0].Rows[0]["a"].ToString()))
                            {
                                dt.Rows[i][j] = dt_jdk.Tables[0].Rows[0]["a"].ToString().ToString();
                            }
                            else
                            {
                                dt.Rows[i][j] = 0;
                            }
                        }
                        else
                        {
                            dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                        }
                    }

                }
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    dt.Rows[m][7] = double.Parse(dt.Rows[m][7].ToString()) + double.Parse(dt.Rows[m][14].ToString());
                    dt.Rows[m][17] = double.Parse(dt.Rows[m][17].ToString()) + double.Parse(dt.Rows[m][19].ToString()) + double.Parse(dt.Rows[m][20].ToString());
                }

            }
            return dt;
        }
        //A3现金使用统计
        public DataTable report_A3_4(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();
            //统计相关信息
            String sql_select_xjsy = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(rydyzc)+sum(zfgtjyzc)+sum(clfzc)+sum(yxzc)+sum(lxzc)+sum(bskzc)+sum(qtzc),sum(rydyzc),sum(zfgtjyzc),sum(clfzc),sum(yxzc),sum(lxzc),sum(bskzc),sum(qtzc) from t_dwxx,t_xjsy where left(t_xjsy.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_xjsy);

            for (int m = 0; m < 11; m++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j < 10; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
            }

            return dt;
        }
        //公务卡统计表
        public DataTable report_A3_5(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            String sql_select_gwk = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(dlhsdw),sum(txgwkjsdw),sum(qdfwxy),sum(azcwposj), sum(azshbxxt), sum(ffgwksl), sum(zzkhdxe), sum(gsyhfk), sum(nyyhfk),sum(zgyhfk),sum(jsyhfk),sum(jtyhfk),sum(qtyhfk),sum(skzfje),sum(xjzfje), sum(bjbsktjdq) ,sum(bjbsktjcs) ,sum(agdzfggr) ,sum(znsyxjjs) ,sum(zxzzdzxrw) ,sum(qtqk) from t_dwxx,t_gwk where left(t_gwk.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_gwk);

            //增加首列排序
            for (int m = 0; m < 26; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j <= 8; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
                dt.Rows[i][9] = int.Parse(ds.Tables[0].Rows[i][9].ToString()) + int.Parse(ds.Tables[0].Rows[i][10].ToString()) + int.Parse(ds.Tables[0].Rows[i][11].ToString()) + int.Parse(ds.Tables[0].Rows[i][12].ToString()) + int.Parse(ds.Tables[0].Rows[i][13].ToString()) + int.Parse(ds.Tables[0].Rows[i][14].ToString());
                for (int k = 10; k < 18; k++)
                {
                    dt.Rows[i][k] = ds.Tables[0].Rows[i][k - 1];
                }
                //dt.Rows[i][16] = String.Format("{0:F}", ds.Tables[0].Rows[i][15].ToString())+"%";
                //dt.Rows[i][16] = ds.Tables[0].Rows[i][15];
                if (double.Parse(ds.Tables[0].Rows[i][15].ToString()) == 0 && double.Parse(ds.Tables[0].Rows[i][16].ToString()) == 0)
                {
                    dt.Rows[i][18] = "0%";
                }
                else
                {
                    dt.Rows[i][18] = Math.Round(double.Parse(ds.Tables[0].Rows[i][15].ToString()) * 100 / (double.Parse(ds.Tables[0].Rows[i][15].ToString()) + double.Parse(ds.Tables[0].Rows[i][16].ToString())), 2).ToString() + "%";

                }
                dt.Rows[i][19] = double.Parse(ds.Tables[0].Rows[i][17].ToString()) + double.Parse(ds.Tables[0].Rows[i][18].ToString()) + double.Parse(ds.Tables[0].Rows[i][19].ToString()) + double.Parse(ds.Tables[0].Rows[i][20].ToString()) + double.Parse(ds.Tables[0].Rows[i][21].ToString()) + double.Parse(ds.Tables[0].Rows[i][22].ToString());
                for (int m = 20; m < 26; m++)
                {
                    dt.Rows[i][m] = ds.Tables[0].Rows[i][m - 3];
                }
            }
            return dt;
        }

        //公务卡统计表（明细）
        public DataTable report_A3_A(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            String sql_select_gwk = "select t_dwxx.dwdm, t_dwxx.dwmc,dlhsdw,txgwkjsdw,qdfwxy,azcwposj, azshbxxt, ffgwksl, zzkhdxe, gsyhfk, nyyhfk,zgyhfk,jsyhfk,jtyhfk,qtyhfk,skzfje,xjzfje, bjbsktjdq ,bjbsktjcs ,agdzfggr, znsyxjjs, zxzzdzxrw, qtqk from t_dwxx, t_gwk where left(t_gwk.dwdm,3)= t_dwxx.dwdm  order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_gwk);

            //增加首列排序
            for (int m = 0; m < 26; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j <= 8; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
                dt.Rows[i][9] = int.Parse(ds.Tables[0].Rows[i][9].ToString()) + int.Parse(ds.Tables[0].Rows[i][10].ToString()) + int.Parse(ds.Tables[0].Rows[i][11].ToString()) + int.Parse(ds.Tables[0].Rows[i][12].ToString()) + int.Parse(ds.Tables[0].Rows[i][13].ToString()) + int.Parse(ds.Tables[0].Rows[i][14].ToString());
                for (int k = 10; k < 18; k++)
                {
                    dt.Rows[i][k] = ds.Tables[0].Rows[i][k - 1];
                }
                //dt.Rows[i][16] = String.Format("{0:F}", ds.Tables[0].Rows[i][15].ToString())+"%";
                //dt.Rows[i][16] = ds.Tables[0].Rows[i][15];
                if (double.Parse(ds.Tables[0].Rows[i][15].ToString()) == 0 && double.Parse(ds.Tables[0].Rows[i][16].ToString()) == 0)
                {
                    dt.Rows[i][18] = "0%";
                }
                else
                {
                    dt.Rows[i][18] = Math.Round(double.Parse(ds.Tables[0].Rows[i][15].ToString()) * 100 / (double.Parse(ds.Tables[0].Rows[i][15].ToString()) + double.Parse(ds.Tables[0].Rows[i][16].ToString())), 2).ToString() + "%";

                }
                dt.Rows[i][19] = double.Parse(ds.Tables[0].Rows[i][17].ToString()) + double.Parse(ds.Tables[0].Rows[i][18].ToString()) + double.Parse(ds.Tables[0].Rows[i][19].ToString()) + double.Parse(ds.Tables[0].Rows[i][20].ToString()) + double.Parse(ds.Tables[0].Rows[i][21].ToString()) + double.Parse(ds.Tables[0].Rows[i][22].ToString());
                for (int m = 20; m < 26; m++)
                {
                    dt.Rows[i][m] = ds.Tables[0].Rows[i][m - 3];
                }
            }
            return dt;
        }

        //A3银行账户清查处理
        public DataTable report_A3_6(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //统计相关信息
            String sql_select_yhzhqccl = "select t_dwxx.dwdm,t_dwxx.dwmc,sum(szkszh),sum(yqzh), sum(ycwczh), sum(qtwgzh), sum(wgccbs), sum(wgccje), sum(wgjdbs), sum(wgjdje), sum(qtyybs), sum(qtyyje),sum(sjfk),  sum(gfsxbs), sum(gfsxje) from t_dwxx,t_yhzhqccl where left(t_yhzhqccl.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc order by t_dwxx.dwdm asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhqccl);

            for (int m = 0; m < 15; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j < 15; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j];
                }
            }
            return dt;
        }


        public DataTable report_A3_9(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();
            //统计相关信息
            String sql_select_xjsy = "select t_xjsy.yf,sum(rydyzc)+sum(zfgtjyzc)+sum(clfzc)+sum(yxzc)+sum(lxzc)+sum(bskzc)+sum(qtzc),sum(rydyzc),sum(zfgtjyzc),sum(clfzc),sum(yxzc),sum(lxzc),sum(bskzc),sum(qtzc),'' from t_xjsy  group by t_xjsy.yf order by t_xjsy.yf asc";
            DataSet ds = AccessHelper.getDataSet(sql_select_xjsy);

            for (int m = 0; m < 11; m++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = (i + 1).ToString();
                for (int j = 1; j < 10; j++)
                {
                    dt.Rows[i][j] = ds.Tables[0].Rows[i][j - 1];
                }
            }

            return dt;
        }
        //A3型报表——往来款项明细统计表
        public DataTable report_A3_7(String reportname)
        {
            DataTable dt_1 = new DataTable();
            dt_1 = report_A3_71("");
            return dt_1;
        }

        //A3型报表——借垫款明细统计表
        public DataTable report_A3_8(String reportname)
        {
            DataTable dt_1 = new DataTable();
            dt_1 = report_A3_81("");

            return dt_1;
        }

        //A3型报表——借垫款明细统计表
        public DataTable report_A3_B(String reportname)
        {
            DataTable dt_1 = new DataTable();
            dt_1 = report_A3_B1("");

            return dt_1;
        }

        public DataTable report_A3_C(String reportname)
        {
            DataTable dt_1 = new DataTable();
            dt_1 = report_A3_C1("");

            return dt_1;
        }

        public DataTable report_A3_71(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            ////获得单位名称
            //String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            //DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "SELECT   t_dwxx.dwmc , t_wlkx.brzcjf, t_wlkx.brzfzxjf , t_wlkx.brzxzj , t_wlkx.bczcjf, t_wlkx.bczfzxjf , t_wlkx.bczxzj , t_wlkx.zbwzcbgz , t_wlkx.bz FROM t_wlkx, t_dwxx WHERE t_wlkx.dwdm = t_dwxx.dwdm  ORDER BY t_wlkx.dwdm,t_wlkx.ID";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 12; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 12; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["brzcjf"].ToString(); //拨入正常军费
                dt.Rows[i][4] = ds.Tables[0].Rows[i]["brzfzxjf"].ToString(); //拨入政府专项经费
                dt.Rows[i][5] = ds.Tables[0].Rows[i]["brzxzj"].ToString(); //拨入专项资金

                dt.Rows[i][7] = ds.Tables[0].Rows[i]["bczcjf"].ToString(); //拨入正常军费
                dt.Rows[i][8] = ds.Tables[0].Rows[i]["bczfzxjf"].ToString(); //拨入正常军费
                dt.Rows[i][9] = ds.Tables[0].Rows[i]["bczxzj"].ToString(); //拨入正常军费
                dt.Rows[i][10] = ds.Tables[0].Rows[i]["zbwzcbgz"].ToString(); //拨入正常军费
                dt.Rows[i][11] = ds.Tables[0].Rows[i]["bz"].ToString(); //拨入正常军费


                dt.Rows[i][2] = (int.Parse(dt.Rows[i][3].ToString()) + int.Parse(dt.Rows[i][4].ToString()) + int.Parse(dt.Rows[i][5].ToString())).ToString();
                dt.Rows[i][6] = (double.Parse(dt.Rows[i][7].ToString()) + double.Parse(dt.Rows[i][8].ToString()) + double.Parse(dt.Rows[i][9].ToString())).ToString();
            }
            return dt;
        }

        public DataTable report_A3_81(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            ////获得单位名称
            //String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            //DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "SELECT  kmmc,xmzy,je,spsj,qx,pzld,bz from t_jdk  ORDER BY t_jdk.dwdm,t_jdk.ID";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 8; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 8; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                dt.Rows[i][1] = ds.Tables[0].Rows[i]["kmmc"].ToString();
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["xmzy"].ToString();
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["je"].ToString();
                dt.Rows[i][4] = ds.Tables[0].Rows[i]["spsj"].ToString();

                dt.Rows[i][5] = ds.Tables[0].Rows[i]["qx"].ToString();
                dt.Rows[i][6] = ds.Tables[0].Rows[i]["pzld"].ToString();
                dt.Rows[i][7] = ds.Tables[0].Rows[i]["bz"].ToString();
            }
            return dt;
        }

        public DataTable report_A3_B1(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            ////获得单位名称
            //String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            //DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "SELECT t_dwxx.dwmc ,t_dqck.zhmc ,t_dqck.hb ,t_dqck.khh ,t_dqck.sfkhh ,t_dqck.cklb ,t_dqck.crrq ,t_dqck.ckqx ,t_dqck.je ,t_dqck.spld ,t_dqck.kj ,t_dqck.cn  FROM t_dwxx,t_dqck where t_dqck.dwdm=t_dwxx.dwdm ORDER BY t_dqck.ID";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 13; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 13; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString();
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["zhmc"].ToString();
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["hb"].ToString();
                dt.Rows[i][4] = ds.Tables[0].Rows[i]["khh"].ToString();

                dt.Rows[i][5] = ds.Tables[0].Rows[i]["sfkhh"].ToString();
                dt.Rows[i][6] = ds.Tables[0].Rows[i]["cklb"].ToString();
                dt.Rows[i][7] = ds.Tables[0].Rows[i]["crrq"].ToString();
                dt.Rows[i][8] = ds.Tables[0].Rows[i]["ckqx"].ToString();
                dt.Rows[i][9] = ds.Tables[0].Rows[i]["je"].ToString();
                dt.Rows[i][10] = ds.Tables[0].Rows[i]["spld"].ToString();
                dt.Rows[i][11] = ds.Tables[0].Rows[i]["kj"].ToString();
                dt.Rows[i][12] = ds.Tables[0].Rows[i]["cn"].ToString();
            }
            return dt;
        }

        public DataTable report_A3_C1(String reportname)
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            ////获得单位名称
            //String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            //DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);

            //统计相关信息
            String sql_select_yhzhtj = "SELECT t_dwxx.dwmc,t_yhzh.zhmc ,t_yhzh.zh ,t_yhzh.zhlb ,t_yhzh.zhxz ,t_yhzh.hb ," + "t_yhzh.khh ,t_yhzh.khhlxr ,t_yhzh.khhlxdh ,t_yhzh.khsj ,t_yhzh.pzkhsj ,t_yhzh.pzkhhzh ,t_yhzh.pzchsj ,t_yhzh.dwfzr ,t_yhzh.dwfzrlxdh ,t_dwxx.szss ,t_yhzh.ckye,t_yhzh.clyj ,t_yhzh.bzyymx from t_yhzh,t_dwxx where t_yhzh.dwdm=t_dwxx.dwdm ORDER BY t_yhzh.ID";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 20; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 20; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString();
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["zhmc"].ToString();
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["zh"].ToString();
                dt.Rows[i][4] = ds.Tables[0].Rows[i]["zhlb"].ToString();

                dt.Rows[i][5] = ds.Tables[0].Rows[i]["zhxz"].ToString();
                dt.Rows[i][6] = ds.Tables[0].Rows[i]["hb"].ToString();
                dt.Rows[i][7] = ds.Tables[0].Rows[i]["khh"].ToString();
                dt.Rows[i][8] = ds.Tables[0].Rows[i]["khhlxr"].ToString();
                dt.Rows[i][9] = ds.Tables[0].Rows[i]["khhlxdh"].ToString();
                dt.Rows[i][10] = ds.Tables[0].Rows[i]["khsj"].ToString();
                dt.Rows[i][11] = ds.Tables[0].Rows[i]["pzkhsj"].ToString();
                dt.Rows[i][12] = ds.Tables[0].Rows[i]["pzkhhzh"].ToString();
                dt.Rows[i][13] = ds.Tables[0].Rows[i]["pzchsj"].ToString();
                dt.Rows[i][14] = ds.Tables[0].Rows[i]["dwfzr"].ToString();
                dt.Rows[i][15] = ds.Tables[0].Rows[i]["dwfzrlxdh"].ToString();
                dt.Rows[i][16] = ds.Tables[0].Rows[i]["szss"].ToString();
                dt.Rows[i][17] = ds.Tables[0].Rows[i]["ckye"].ToString();
                dt.Rows[i][18] = ds.Tables[0].Rows[i]["clyj"].ToString();
                dt.Rows[i][19] = ds.Tables[0].Rows[i]["bzyymx"].ToString();
            }
            return dt;
        }

        //本级单位明细
        public DataTable report_BJDW()
        {
            DataTable dt_1 = new DataTable();
            dt_1 = report_A3_BJDW();
            return dt_1;
        }


        public DataTable report_WPFBQKTJ()
        {
            DataTable dt_1 = new DataTable();
            dt_1 = report_A3_WPFBQKTJ();
            return dt_1;
        }

        public DataTable report_A3_WPFBQKTJ()
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //统计相关信息
            String sql_select_yhzhtj = "SELECT   t_dwxx.dwmc,t_dwxx.szss, t_dwxx.szs,Switch(t_dwxx.dwjb='','0',t_dwxx.dwjb='军委机关部门','1',t_dwxx.dwjb='正战区级','2',t_dwxx.dwjb='副战区级','3',t_dwxx.dwjb='正军级','4',t_dwxx.dwjb='副军级','5',t_dwxx.dwjb='正师级','6',t_dwxx.dwjb='副师级','7',t_dwxx.dwjb='正团级','8',t_dwxx.dwjb='副团级','9',True,'10') AS dwjb, Sum(t_lctc.sl) AS sl  FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm GROUP BY t_dwxx.szss, t_dwxx.szs, dwjb,t_dwxx.dwmc; ";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 6; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 6; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();
                dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称

                //}
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["szss"].ToString(); //所在省
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["szs"].ToString(); //所在市
                string dwjb = string.Empty;
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["dwjb"].ToString()))
                {
                    switch (ds.Tables[0].Rows[i]["dwjb"].ToString())
                    {
                        case "0":
                            dwjb = "";
                            break;
                        case "1":
                            dwjb = "军委机关部门";
                          break;

                        case "2":
                            dwjb = "正战区级";
                            break;
                        case "3":
                            dwjb = "副战区级";
                            break;
                        case "4":
                            dwjb = "正军级";
                            break;
                        case "5":
                            dwjb = "副军级";
                            break;
                        case "6":
                            dwjb = "正师级";
                            break;
                        case "7":
                            dwjb = "副师级";
                            break;
                        case "8":
                            dwjb = "正团级";
                            break;
                        case "9":
                            dwjb = "副团级";
                            break;
                        default:
                            dwjb = "营以下单位";
                            break;
                    }
                }
                dt.Rows[i][4] = dwjb; //单位级别
                dt.Rows[i][5] = ds.Tables[0].Rows[i]["sl"].ToString(); //商品数量
            }
            return dt;
        }

        public DataTable report_A3_BJDW()
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            ////获得单位名称
            //String sql_dwmc = "select dwmc from t_dwxx where len(dwdm)=3 order by dwdm asc";
            //DataSet ds_1 = AccessHelper.getDataSet(sql_dwmc);



            //统计相关信息
            String sql_select_yhzhtj = " SELECT t_lctc.dwdm,t_bm.bmmc ,t_lctc.lb, t_lctc.pm, t_lctc.ly, t_lctc.hqsj, t_lctc.sl, t_lctc.jldw, t_lctc.djlx, t_lctc.dj , t_lctc.zz, t_lctc.kysl, t_lctc.kbxjz, t_lctc.czfs, t_lctc.bz,t_lctc.bmbs FROM t_lctc left join t_bm on t_lctc.bmbs = t_bm.bmbs where t_lctc.dwdm='000' ORDER BY t_lctc.dwdm, t_lctc.ID;";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 15; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //string bmmc = "";
                //string get_bmmc = "select bmmc from t_bm where bmbs='"+ ds.Tables[0].Rows[i]["bmbs"] + "'";
                //DataTable dt_bmmc = AccessHelper.getDataSet(get_bmmc).Tables[0];
                //if (dt_bmmc.Rows.Count > 0)
                //{
                //    bmmc = dt_bmmc.Rows[0]["bmmc"].ToString();
                //}
                dt.Rows.Add();
                for (int j = 0; j < 15; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                //if (ds.Tables[0].Rows[i]["dwdm"].ToString() == "000")
                //{
                //    dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString() + "(本级)"; //单位(部门名称)名称

                //}
                //else
                //{
                    dt.Rows[i][1] = ds.Tables[0].Rows[i]["bmmc"].ToString(); //单位(部门名称)名称

                //}
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["lb"].ToString(); //类别
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["pm"].ToString(); //品名
                dt.Rows[i][4] = ds.Tables[0].Rows[i]["ly"].ToString(); //来源

                dt.Rows[i][5] = ds.Tables[0].Rows[i]["hqsj"].ToString(); //获取时间
                dt.Rows[i][6] = Convert.ToDecimal(ds.Tables[0].Rows[i]["sl"].ToString()).ToString("0.#####"); //数量
                dt.Rows[i][7] = ds.Tables[0].Rows[i]["jldw"].ToString(); //计量单位
                dt.Rows[i][8] = ds.Tables[0].Rows[i]["djlx"].ToString(); //单价
                dt.Rows[i][9] = ds.Tables[0].Rows[i]["dj"].ToString(); //单价
                dt.Rows[i][10] = ds.Tables[0].Rows[i]["zz"].ToString(); //总值
                dt.Rows[i][11] = Convert.ToDecimal(ds.Tables[0].Rows[i]["kysl"].ToString()).ToString("0.#####"); //堪用数量
                dt.Rows[i][12] = ds.Tables[0].Rows[i]["kbxjz"].ToString(); //可变现价值
                dt.Rows[i][13] = ds.Tables[0].Rows[i]["czfs"].ToString(); //处置方式
                dt.Rows[i][14] = ds.Tables[0].Rows[i]["bz"].ToString(); //备注
            }
            return dt;
        }

        public DataTable report_SUM()
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();
            string filterStr = string.Empty;
            string sql_select_yhzhtj = "select t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.bmbs ,sum(t_lctc.kysl) as sl, sum(t_lctc.zz) as zz, sum(t_lctc.kysl) as kysl, sum(t_lctc.kbxjz) as kbxjz from t_lctc  WHERE t_lctc.dwdm ='000'   group by t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.bmbs";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 8; m++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 8; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                string sql_dw = "select t_bm.bmmc from t_bm where bmbs='"+ ds.Tables[0].Rows[i]["bmbs"] + "'";
                DataTable dt_dw = AccessHelper.getDataSet(sql_dw).Tables[0];
                if (dt_dw.Rows.Count > 0)
                {

                    dt.Rows[i][1] = dt_dw.Rows[0]["bmmc"].ToString(); //单位名称
                }
                else
                {
                    dt.Rows[i][1] = "";
                }
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["lb"].ToString(); //类别
                dt.Rows[i][3] = Convert.ToDecimal(ds.Tables[0].Rows[i]["sl"].ToString()).ToString("0.#####"); //数量

                dt.Rows[i][4] = ds.Tables[0].Rows[i]["zz"].ToString(); //总值

                dt.Rows[i][5] = Convert.ToDecimal(ds.Tables[0].Rows[i]["kysl"].ToString()).ToString("0.#####"); //刊用数量
                dt.Rows[i][6] = ds.Tables[0].Rows[i]["kbxjz"].ToString(); //可变现价值
                dt.Rows[i][7] = ds.Tables[0].Rows[i]["czfs"].ToString(); //处置方式

            }
            return dt;
        }

        public DataTable report_SUMALL()
        {
            dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();
            string filterStr = string.Empty;
            string sql_select_yhzhtj = "select t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.bmbs ,sum(t_lctc.sl) as sl, sum(t_lctc.zz) as zz, sum(t_lctc.kysl) as kysl, sum(t_lctc.kbxjz) as kbxjz from t_lctc    group by t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.bmbs order by t_lctc.dwdm";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 8; m++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 8; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();//序号
                string sql_dw = "select t_dwxx.dwmc,t_dwxx.dwdm from t_dwxx where dwdm='" + ds.Tables[0].Rows[i]["dwdm"] + "'";
                DataTable dt_dw = AccessHelper.getDataSet(sql_dw).Tables[0];
                if (dt_dw.Rows.Count > 0)
                {
                    dt.Rows[i][1] = dt_dw.Rows[0]["dwmc"].ToString()+"本级"; //单位名称
                }
                else
                {
                    dt.Rows[i][1] = "";
                }
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["lb"].ToString(); //类别
                dt.Rows[i][3] = Convert.ToDecimal(ds.Tables[0].Rows[i]["sl"].ToString()).ToString("0.#####") ; //数量
                dt.Rows[i][4] = ds.Tables[0].Rows[i]["zz"].ToString(); //总值

                dt.Rows[i][5] = Convert.ToDecimal(ds.Tables[0].Rows[i]["kysl"].ToString()).ToString("0.#####"); //刊用数量
                dt.Rows[i][6] = ds.Tables[0].Rows[i]["kbxjz"].ToString(); //可变现价值
                dt.Rows[i][7] = ds.Tables[0].Rows[i]["czfs"].ToString(); //处置方式

            }
            return dt;
        }

    }
}
