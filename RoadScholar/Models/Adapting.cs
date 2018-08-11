using RoadScholar.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadScholar.Models
{
    public class Adapting
    {
        public static Student getStudentFromData(StudentData sData)
        {
            Student s = new Student();
            s.PhoneNumber = sData.PhoneNumber;
            s.FirstName = sData.FirstName;
            s.LastName = sData.LastName;
            s.Email = sData.Email;
            s.RoomId = sData.RoomId;
            return s;
        }

        public static Instruction getInstructionFromData(InstructionData instructionData)
        {
            Instruction i = new Instruction();
            i.ActivityName = instructionData.ActivityName;
            i.expID = instructionData.expID;
            i.id = instructionData.id;
            i.isMainActivity = instructionData.isMainActivity;
            i.RoomId = instructionData.RoomId;
            i.Type = instructionData.Type;

            i.Command = instructionData.Command;

            return i;
        }

        public static List<Activity> getActivityListFromData(List<ActivityData> lstData)
        {
            List<Activity> lst = new List<Activity>();
            foreach (ActivityData actData in lstData)
            {
                lst.Add(getActivityFromData(actData));
            }
            return lst;
        }

        public static List<StudentData> getStudentListAsData(List<Student> lstStudents)
        {
            List<StudentData> lst = new List<StudentData>();
            foreach (Student student in lstStudents)
            {
                lst.Add(getStudentAsData(student));
            }
            return lst;
        }

        public static List<ActivityLog> getActivityLogListFromData(List<ActivityLogData> lstData)
        {
            List<ActivityLog> lst = new List<ActivityLog>();
            foreach (ActivityLogData alData in lstData)
            {
                lst.Add(getActivityLogFromData(alData));
            }
            return lst;
        }

        public static Teacher getTeacherFromData(TeacherData teacherData)
        {
            Teacher t = new Teacher();
            t.PhoneNumber = teacherData.PhoneNumber;
            t.FirstName = teacherData.FirstName;
            t.LastName = teacherData.LastName;
            t.Email = teacherData.Email;
            t.Password = teacherData.Password;
            t.RoomId = teacherData.RoomId;
            return t;
        }

        public static object getStudentListFromData(List<StudentData> lstData)
        {
            List<Student> lst = new List<Student>();
            foreach (StudentData studentData in lstData)
            {
                lst.Add(getStudentFromData(studentData));
            }
            return lst;
        }

       

        public static AmericanQuestion getAmericanQuestionFromData(AmericanQuestionData aqData)
        {
            AmericanQuestion aq = new AmericanQuestion();
            aq.ActivityName = aqData.ActivityName;
            aq.expID = aqData.expID;
            aq.id = aqData.id;
            aq.isMainActivity = aqData.isMainActivity;
            aq.RoomId = aqData.RoomId;
            aq.Type = aqData.Type;

            aq.question = aqData.question;
            aq.explaination = aqData.explaination;

            aq.counterFirst = aqData.counterFirst;
            aq.counterSecond = aqData.counterSecond;
            aq.counterThird = aqData.counterThird;
            aq.counterFourth = aqData.counterFourth;
            aq.correctAnswer = aqData.correctAnswer;

            aq.firstAnswer = aqData.firstAnswer;
            aq.secondAnswer = aqData.secondAnswer;
            aq.thirdAnswer = aqData.thirdAnswer;
            aq.fourthAnswer = aqData.fourthAnswer;

            aq.studentsAnswers = new List<AnswerByPhone>();
            if (aqData.studentsAnswers != null)
            {
                foreach (AnswerByPhoneData abpData in aqData.studentsAnswers)
                {
                    aq.studentsAnswers.Add(getAnswerByPhoneFromData(abpData));
                }
            }
            return aq;
        }

        public static TFQLog getTFQLogFromData(TFQLogData tfqlData)
        {
            TFQLog tfql = new TFQLog();

            tfql.ActivityName = tfqlData.ActivityName;
            tfql.expID = tfqlData.expID;
            tfql.id = tfqlData.id;
            tfql.isMainActivity = tfqlData.isMainActivity;
            tfql.RoomId = tfqlData.RoomId;

            tfql.question = tfqlData.question;
            tfql.explaination = tfqlData.explaination;

            tfql.correctAnswerBool = tfqlData.correctAnswerBool;
            tfql.counterFalse = tfqlData.counterFalse;
            tfql.counterTrue = tfqlData.counterTrue;

            tfql.studentsAnswers = new List<AnswerByPhone>();
            if (tfqlData.studentsAnswers != null)
            {
                foreach (AnswerByPhoneData abpData in tfqlData.studentsAnswers)
                {
                    tfql.studentsAnswers.Add(getAnswerByPhoneFromData(abpData));
                }
            }

            return tfql;
        }

        public static AnswerByPhone getAnswerByPhoneFromData(AnswerByPhoneData abpData)
        {
            AnswerByPhone abp = new AnswerByPhone();
            abp.ActivityId = abpData.ActivityId;
            abp.Answer = abpData.Answer;
            abp.Id = abpData.Id;
            abp.Phone = abpData.Phone;
            abp.RoomID = abpData.RoomID;
            return abp;
        }

        public static ActivityLog getActivityLogFromData(ActivityLogData activityLogData)
        {
            ActivityLog al = new ActivityLog();
            al.ActivityName = activityLogData.ActivityName;
            al.date = activityLogData.date;
            al.expID = activityLogData.expID;
            al.id = activityLogData.id;
            al.isMainActivity = activityLogData.isMainActivity;
            al.RoomId = activityLogData.RoomId;

            return al;
        }

        public static ExperimentLog getExperimentLogFromData(ExperimentLogData elData)
        {
            ExperimentLog el = new ExperimentLog();
            el.ActivityName = elData.ActivityName;
            el.date = elData.date;
            el.expID = elData.expID;
            el.id = elData.id;
            el.isMainActivity = elData.isMainActivity;
            el.RoomId = elData.RoomId;
            el.ExperimentQuestion = elData.ExperimentQuestion;
            el.ActiveExpID = elData.ActiveExpID;
            el.NumOfMeasuresGraph = elData.NumOfMeasuresGraph;
            el.DiffBetweenMeasuresGraph = elData.DiffBetweenMeasuresGraph;

            return el;
        }

        public static AmericanLog getAmericanLogFromData(AmericanLogData aqlData)
        {
            AmericanLog aql = new AmericanLog();

            aql.ActivityName = aqlData.ActivityName;
            aql.expID = aqlData.expID;
            aql.id = aqlData.id;
            aql.isMainActivity = aqlData.isMainActivity;
            aql.RoomId = aqlData.RoomId;

            aql.question = aqlData.question;
            aql.explaination = aqlData.explaination;

            aql.correctAnswer = aqlData.correctAnswer;
            aql.counterFirst = aqlData.counterFirst;
            aql.counterSecond = aqlData.counterSecond;
            aql.counterThird = aqlData.counterThird;
            aql.counterFourth = aqlData.counterFourth;

            aql.firstAnswer = aqlData.firstAnswer;
            aql.secondAnswer = aqlData.secondAnswer;
            aql.thirdAnswer = aqlData.thirdAnswer;
            aql.fourthAnswer = aqlData.fourthAnswer;

            aql.studentsAnswers = new List<AnswerByPhone>();
            if (aqlData.studentsAnswers != null)
            {
                foreach (AnswerByPhoneData abpData in aqlData.studentsAnswers)
                {
                    aql.studentsAnswers.Add(getAnswerByPhoneFromData(abpData));
                }
            }
            return aql;
        }

        public static SALog getShortAnswerQuestionLogFromData(SALogData saqLogData)
        {
            SALog saqLog = new SALog();
            saqLog.ActivityName = saqLogData.ActivityName;
            saqLog.expID = saqLogData.expID;
            saqLog.id = saqLogData.id;
            saqLog.RoomId = saqLogData.RoomId;

            saqLog.question = saqLogData.question;
            saqLog.explaination = saqLogData.explaination;

            saqLog.correctAnswerString = saqLogData.correctAnswerString;

            saqLog.studentsAnswers = new List<AnswerByPhone>();
            if (saqLogData.studentsAnswers != null)
            {
                foreach (AnswerByPhoneData abpData in saqLogData.studentsAnswers)
                {
                    saqLog.studentsAnswers.Add(getAnswerByPhoneFromData(abpData));
                }
            }
            return saqLog;
        }

        

        public static TrueFalseQuestion getTrueFalseQuestionFromData(TrueFalseQuestionData tfqData)
        {
            TrueFalseQuestion tfq = new TrueFalseQuestion();
            tfq.ActivityName = tfqData.ActivityName;
            tfq.expID = tfqData.expID;
            tfq.id = tfqData.id;
            tfq.isMainActivity = tfqData.isMainActivity;
            tfq.RoomId = tfqData.RoomId;
            tfq.Type = tfqData.Type;

            tfq.question = tfqData.question;
            tfq.explaination = tfqData.explaination;

            tfq.correctAnswerBool = tfqData.correctAnswerBool;
            tfq.counterFalse = tfqData.counterFalse;
            tfq.counterTrue = tfqData.counterTrue;

            tfq.studentsAnswers = new List<AnswerByPhone>();
            if (tfqData.studentsAnswers != null)
            {
                foreach (AnswerByPhoneData abpData in tfqData.studentsAnswers)
                {
                    tfq.studentsAnswers.Add(getAnswerByPhoneFromData(abpData));
                }
            }
            return tfq;
        }

        public static StudentData getStudentAsData(Student s)
        {
            StudentData sData = new StudentData();
            sData.PhoneNumber = s.PhoneNumber;
            sData.FirstName = s.FirstName;
            sData.LastName = s.LastName;
            sData.Email = s.Email;
            sData.RoomId = s.RoomId;
            return sData;
        }

        public static Experiment getExperimentFromData(ExperimentData expData)
        {
            Experiment exp = new Experiment();
            exp.ActivityName = expData.ActivityName;
            exp.expID = expData.expID;
            exp.id = expData.id;
            exp.isMainActivity = expData.isMainActivity;
            exp.RoomId = expData.RoomId;

            exp.ExperimentQuestion = expData.ExperimentQuestion;
            exp.Type = expData.Type;
            exp.ActiveExpID = expData.ActiveExpID;
            exp.activities = new List<Activity>();
            if (expData.activities != null)
            {
                foreach (ActivityData actData in expData.activities)
                {
                    exp.activities.Add(getActivityFromData(actData));
                }
            }
            return exp;
        }

        public static Room getRoomFromData(RoomData roomData)
        {
            Room r = new Room();
            r.CurrentActivityId = roomData.CurrentActivityId;
            r.id = roomData.id;
            return r;
        }

        public static MeasurementData getMeasurementAsData(Measurement measurement)
        {
            MeasurementData mData = new MeasurementData();
            mData.ActivityName = measurement.ActivityName;
            mData.expID = measurement.expID;
            mData.id = measurement.id;
            mData.isMainActivity = measurement.isMainActivity;
            mData.RoomId = measurement.RoomId;

            mData.NumOfMeasures = measurement.NumOfMeasures;
            mData.DifferenceBetweenMeasures = measurement.DifferenceBetweenMeasures;
            mData.NumOfParametersToMeasure = measurement.NumOfParametersToMeasure;
            mData.WindowOpenTimeSeconds = measurement.WindowOpenTimeSeconds;
            mData.experimentOrder = measurement.experimentOrder;

            if (measurement.measurementInstructions != null)
            {
                foreach (MeasureInstruction mi in measurement.measurementInstructions)
                {
                    mData.measurementInstructions.Add(getMeasureInstructionAsData(mi));
                }
            }

            return mData;
        }

        public static MeasureInstructionData getMeasureInstructionAsData(MeasureInstruction mi)
        {
            MeasureInstructionData mid = new MeasureInstructionData();
            mid.id = mi.id;
            mid.MeasurementInstruction = mi.MeasurementInstruction;
            return mid;
        }

        public static Activity getActivityFromData(ActivityData activityData)
        {
            if (activityData == null)
                return null;
            Activity a = new Activity();
            a.ActivityName = activityData.ActivityName;
            a.expID = activityData.expID;
            a.id = activityData.id;
            a.isMainActivity = activityData.isMainActivity;
            a.RoomId = activityData.RoomId;
            a.Type = activityData.Type;
            return a;
        }

        public static ExperimentData getExperimentAsData(Experiment exp)
        {
            ExperimentData expData = new ExperimentData();
            expData.ActivityName = exp.ActivityName;
            expData.expID = exp.expID;
            expData.id = exp.id;
            expData.RoomId = exp.RoomId;

            expData.ExperimentQuestion = exp.ExperimentQuestion;
            expData.activities = new List<ActivityData>();
            if (exp.activities != null)
            {
                foreach (Activity act in exp.activities)
                {
                    expData.activities.Add(getActivityAsData(act));
                }
            }
            return expData;
        }

        private static ActivityData getActivityAsData(Activity act)
        {
            ActivityData aData = new ActivityData();
            aData.ActivityName = act.ActivityName;
            aData.expID = act.expID;
            aData.id = act.id;
            aData.isMainActivity = act.isMainActivity;
            aData.RoomId = act.RoomId;
            aData.Type = act.Type;
            return aData;
        }

        public static InstructionData getInstructionAsData(Instruction instruction)
        {
            InstructionData iData = new InstructionData();
            iData.ActivityName = instruction.ActivityName;
            iData.expID = instruction.expID;
            iData.id = instruction.id;
            iData.isMainActivity = instruction.isMainActivity;
            iData.RoomId = instruction.RoomId;

            iData.Command = instruction.Command;

            return iData;
        }

        public static ImageData getImageAsData(Image image)
        {
            ImageData iData = new ImageData();
            iData.ActivityName = image.ActivityName;
            iData.expID = image.expID;
            iData.id = image.id;
            iData.isMainActivity = image.isMainActivity;
            iData.RoomId = image.RoomId;

            iData.Title = image.Title;
            iData.URL = image.URL;

            return iData;
        }

        public static VideoData getVideoAsData(Video video)
        {
            VideoData vData = new VideoData();
            vData.ActivityName = video.ActivityName;
            vData.expID = video.expID;
            vData.id = video.id;
            vData.isMainActivity = video.isMainActivity;
            vData.RoomId = video.RoomId;

            vData.URL = video.URL;

            return vData;
        }

        public static TrueFalseQuestionData getTrueFalseQuestionAsData(TrueFalseQuestion tfq)
        {
            TrueFalseQuestionData tfqData = new TrueFalseQuestionData();
            tfqData.ActivityName = tfq.ActivityName;
            tfqData.expID = tfq.expID;
            tfqData.id = tfq.id;
            tfqData.RoomId = tfq.RoomId;

            tfqData.question = tfq.question;
            tfqData.explaination = tfq.explaination;

            tfqData.correctAnswerBool = tfq.correctAnswerBool;
            tfqData.counterFalse = tfq.counterFalse;
            tfqData.counterTrue = tfq.counterTrue;

            tfqData.studentsAnswers = new List<AnswerByPhoneData>();
            if (tfq.studentsAnswers != null)
            {
                foreach (AnswerByPhone abp in tfq.studentsAnswers)
                {
                    tfqData.studentsAnswers.Add(getAnswerByPhoneAsData(abp));
                }
            }
            return tfqData;
        }

        private static AnswerByPhoneData getAnswerByPhoneAsData(AnswerByPhone abp)
        {
            AnswerByPhoneData abpData = new AnswerByPhoneData();
            abpData.ActivityId = abp.ActivityId;
            abpData.Answer = abp.Answer;
            abpData.Id = abp.Id;
            abpData.Phone = abp.Phone;
            return abpData;
        }

        public static ShortAnswerQuestionData getShortAnswerQuestionAsData(ShortAnswerQuestion saq)
        {
            ShortAnswerQuestionData saqData = new ShortAnswerQuestionData();
            saqData.ActivityName = saq.ActivityName;
            saqData.expID = saq.expID;
            saqData.id = saq.id;
            saqData.RoomId = saq.RoomId;

            saqData.question = saq.question;
            saqData.explaination = saq.explaination;

            saqData.correctAnswerString = saq.correctAnswerString;

            saqData.studentsAnswers = new List<AnswerByPhoneData>();
            if (saq.studentsAnswers != null)
            {
                foreach (AnswerByPhone abp in saq.studentsAnswers)
                {
                    saqData.studentsAnswers.Add(getAnswerByPhoneAsData(abp));
                }
            }
            return saqData;
        }

        public static AmericanQuestionData getAmericanQuestionAsData(AmericanQuestion aq)
        {
            AmericanQuestionData aqData = new AmericanQuestionData();
            aqData.ActivityName = aq.ActivityName;
            aqData.expID = aq.expID;
            aqData.id = aq.id;
            aqData.RoomId = aq.RoomId;

            aqData.question = aq.question;
            aqData.explaination = aq.explaination;

            aqData.counterFirst = aq.counterFirst;
            aqData.counterSecond = aq.counterSecond;
            aqData.counterThird = aq.counterThird;
            aqData.counterFourth = aq.counterFourth;
            aqData.correctAnswer = aq.correctAnswer;

            aqData.firstAnswer = aq.firstAnswer;
            aqData.secondAnswer = aq.secondAnswer;
            aqData.thirdAnswer = aq.thirdAnswer;
            aqData.fourthAnswer = aq.fourthAnswer;

            aqData.studentsAnswers = new List<AnswerByPhoneData>();
            if (aq.studentsAnswers != null)
            {
                foreach (AnswerByPhone abp in aq.studentsAnswers)
                {
                    aqData.studentsAnswers.Add(getAnswerByPhoneAsData(abp));
                }
            }
            return aqData;
        }

        public static ActiveExperimentData getActiveExperimentAsData(ActiveExperiment ae)
        {
            ActiveExperimentData aeData = new ActiveExperimentData();
            aeData.ActiveExpID = ae.ActiveExpID;
            aeData.ExpID = ae.ExpID;
            aeData.MaxStudentPerGroup = ae.MaxStudentPerGroup;
            aeData.NumberOfGroups = ae.NumberOfGroups;
            return aeData;
        }

        public static ActiveExperiment getActiveExperimentFromData(ActiveExperimentData aeData)
        {
            ActiveExperiment ae = new ActiveExperiment();
            ae.ActiveExpID = aeData.ActiveExpID;
            ae.ExpID = aeData.ExpID;
            ae.MaxStudentPerGroup = aeData.MaxStudentPerGroup;
            ae.NumberOfGroups = aeData.NumberOfGroups;
            return ae;
        }

        public static ShortAnswerQuestion getShortAnswerQuestionFromData(ShortAnswerQuestionData saqData)
        {
            ShortAnswerQuestion saq = new ShortAnswerQuestion();
            saq.ActivityName = saqData.ActivityName;
            saq.expID = saqData.expID;
            saq.id = saqData.id;
            saq.RoomId = saqData.RoomId;

            saq.question = saqData.question;
            saq.explaination = saqData.explaination;

            saq.correctAnswerString = saqData.correctAnswerString;

            saq.studentsAnswers = new List<AnswerByPhone>();
            if (saqData.studentsAnswers != null)
            {
                foreach (AnswerByPhoneData abpData in saqData.studentsAnswers)
                {
                    saq.studentsAnswers.Add(getAnswerByPhoneFromData(abpData));
                }
            }
            return saq;
        }
    }
}