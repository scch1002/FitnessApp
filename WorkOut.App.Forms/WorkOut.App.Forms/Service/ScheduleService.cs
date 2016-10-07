using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using Xamarin.Forms;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.Service
{
    public static class ScheduleService
    {
        public static Session CreateNextSession(SessionDefinition sessionDefinition)
        {
            var warmupWorkOutDefinitions = WorkOutDefinitionRepository.GetWorkOutDefinitions(sessionDefinition.SessionDefinitonId, 1);

            var workOutDefinitions = WorkOutDefinitionRepository.GetWorkOutDefinitions(sessionDefinition.SessionDefinitonId, 0);

            sessionDefinition.SessionWarmUpWorkOuts = new ObservableCollection<WorkOutDefinition>(warmupWorkOutDefinitions);

            sessionDefinition.SessionWorkOuts = new ObservableCollection<WorkOutDefinition>(workOutDefinitions);

            var nextSession = CreateSessionFromDefinition(sessionDefinition);

            return nextSession;
        }

        private static Session CreateSessionFromDefinition(SessionDefinition sessionDefinition)
        {
            return new Session
            {
                SessionDefinitionId = sessionDefinition.SessionDefinitonId,
                SessionName = sessionDefinition.SessionName,
                SessionDate = DateTime.Now,
                SessionWarmUpWorkOuts = new ObservableCollection<ModelWorkOut>(sessionDefinition.SessionWarmUpWorkOuts.Select(s => new ModelWorkOut
                {
                    WorkOutName = s.WorkOutName,
                    WorkOutDefinitionId = s.WorkOutId,
                    WorkOutType = s.WorkOutType.WorkOutType,
                    WorkOutWarmUpSets = CreateWarmUpSetsFromWorkOutDefinition(s),
                    WorkOutSets = CreateSetsFromWorkOutDefinition(s)
                })),
                SessionWorkOuts = new ObservableCollection<ModelWorkOut>(sessionDefinition.SessionWorkOuts.Select(s => new ModelWorkOut
                {
                    WorkOutName = s.WorkOutName,
                    WorkOutDefinitionId = s.WorkOutId,
                    WorkOutType = s.WorkOutType.WorkOutType,
                    WorkOutWarmUpSets = CreateWarmUpSetsFromWorkOutDefinition(s),
                    WorkOutSets = CreateSetsFromWorkOutDefinition(s)
                }))
            };
        }

        private static ObservableCollection<Set> CreateWarmUpSetsFromWorkOutDefinition(WorkOutDefinition workOutDefinition)
        {
            var warmUpWorkOutSets = new ObservableCollection<Set>();

            for (var count = 0; count < workOutDefinition.NumberOfWarmUpSets; count++)
            {
                warmUpWorkOutSets.Add(new Set
                {
                    SetName = "Set " + (count + 1),
                    SetType = 1,
                    Weight = workOutDefinition.WarmUpWeight + (workOutDefinition.WarmUpWeightIncrement * count),
                    CompletedRepetitions = 0,
                    TotalRepetitions = workOutDefinition.WarmUpRepetitions
                });
            }

            return warmUpWorkOutSets;
        }

        private static ObservableCollection<Set> CreateSetsFromWorkOutDefinition(WorkOutDefinition workOutDefinition)
        {
            var workOutSets = new ObservableCollection<Set>();

            for (var count = 0; count < workOutDefinition.NumberOfSets; count++)
            {
                workOutSets.Add(new Set
                {
                    SetName = "Set " + (count + 1),
                    SetType = 0,
                    Weight = workOutDefinition.Weight + (workOutDefinition.WeightIncrement * count),
                    CompletedRepetitions = 0,
                    TotalRepetitions = workOutDefinition.Repetitions
                });
            }

            return workOutSets;
        }
    }
}
