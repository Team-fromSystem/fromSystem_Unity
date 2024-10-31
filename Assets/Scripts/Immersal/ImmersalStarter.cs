/*===============================================================================
Copyright (C) 2024 Immersal - Part of Hexagon. All Rights Reserved.

This file is part of the Immersal SDK.

The Immersal SDK cannot be copied, distributed, or made available to
third-parties for commercial purposes without written permission of Immersal Ltd.

Contact sales@immersal.com for licensing requests.
===============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using Immersal.XR;
using Immersal.REST;

namespace Immersal.Samples
{
    public enum LocalizationMethodChoice
    {
        OnDevice,
        OnServer
    }
    
    public class ImmersalStarter : MonoBehaviour
    {
        [SerializeField]
        private LocalizationMethodChoice m_LocMethodChoice = LocalizationMethodChoice.OnDevice; 
        
        private List<SDKJob> m_Maps;
        private List<IJobAsync> m_Jobs = new List<IJobAsync>();
        private int m_JobLock = 0;

        private MapLoadingOption m_MLO = new MapLoadingOption { DownloadVisualizationAtRuntime = true, m_SerializedDataSource = (int)MapDataSource.Download};

        public int mapId=0;

        [SerializeField] private ImmersalData immersalData;

        void Start()
        {   
            m_MLO.m_SerializedDataSource = m_LocMethodChoice == LocalizationMethodChoice.OnDevice
                ? (int)MapDataSource.Download
                : -1;

            m_Maps = new List<SDKJob>();
            
            Invoke("GetMaps", 0.5f);
        }

        void Update()
        {
            if (m_JobLock == 1)
                return;
            
            if (m_Jobs.Count > 0)
            {
                m_JobLock = 1;
                RunJob(m_Jobs[0]);
            }
        }

        public void GetMaps()
        {
            JobListJobsAsync j = new JobListJobsAsync();
            j.token = ImmersalSDK.Instance.developerToken;
            j.OnResult += (SDKJobsResult result) =>
            {
                if (result.count <= 0) return;
                List<string> names = new List<string>();

                foreach (SDKJob job in result.jobs)
                {
                    if (job.type != (int)SDKJobType.Alignment && (job.status == SDKJobState.Sparse || job.status == SDKJobState.Done))
                    {
                        this.m_Maps.Add(job);
                        names.Add(job.name);
                    }
                }
            };

            m_Jobs.Add(j);

            LoadMap();
        }

        public void ClearMaps()
        {
            MapManager.RemoveAllMaps(true, true);
        }

        private void LoadMap()
        {
            JobMapMetadataGetAsync j = new JobMapMetadataGetAsync();
            j.id = immersalData.chosenImmersalManager.immersalMapManager[0].mapID;
            j.OnResult += async (SDKMapMetadataGetResult result) =>
            {
                MapCreationParameters parameters = new MapCreationParameters
                {
                    MetadataGetResult = result,
                    LocalizationMethodType = m_LocMethodChoice == LocalizationMethodChoice.OnDevice
                        ? typeof(DeviceLocalization)
                        : typeof(ServerLocalization),
                    MapOptions = new IMapOption[] { m_MLO }
                };
                MapCreationResult r = await MapManager.TryCreateMap(parameters);
            };
            m_Jobs.Add(j);
        }

        private async void RunJob(IJobAsync j)
        {
            await j.RunJob();
            if (m_Jobs.Count > 0)
                m_Jobs.RemoveAt(0);
            m_JobLock = 0;
        }
    }
}