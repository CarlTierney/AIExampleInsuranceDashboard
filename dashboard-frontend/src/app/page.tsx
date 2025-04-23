"use client"

import { useEffect, useState } from "react";


// Types for config
interface KpiPanelConfig {
  id: string;
  label: string;
  endpoint: string;
}
interface FilterConfig {
  id: string;
  label: string;
  type: string;
  endpoint: string;
}
interface MainPanelConfig {
  id: string;
  label: string;
  type: string;
  endpoint: string;
}
interface DashboardConfig {
  kpiPanels: KpiPanelConfig[];
  filters: FilterConfig[];
  mainPanels: MainPanelConfig[];
}

// Simple fetcher (replace with SWR or react-query for production)
async function fetchData(endpoint: string) {
  const res = await fetch(endpoint);
  if (!res.ok) throw new Error("Failed to fetch " + endpoint);
  return res.json();
}

function KpiPanel({ config }: { config: KpiPanelConfig }) {
  const [value, setValue] = useState<string | number>("-");
  useEffect(() => {
    fetchData(config.endpoint).then(data => setValue(data.value ?? "-"));
  }, [config.endpoint]);
  return (
    <div className="bg-white shadow rounded p-4 flex flex-col items-center">
      <div className="text-2xl font-bold">{value}</div>
      <div className="text-xs text-gray-500">{config.label}</div>
    </div>
  );
}

function FilterBar({ configs }: { configs: FilterConfig[] }) {
  // For demo, just render dropdowns with static options
  return (
    <div className="flex gap-4 mb-4">
      {configs.map(f => (
        <select key={f.id} className="border rounded px-2 py-1">
          <option>{f.label}</option>
        </select>
      ))}
    </div>
  );
}

function MainPanel({ config }: { config: MainPanelConfig }) {
  // Placeholder for chart/panel rendering
  return (
    <div className="bg-white shadow rounded p-6 min-h-[200px] flex flex-col items-center justify-center">
      <div className="font-semibold mb-2">{config.label}</div>
      <div className="text-gray-400">[{config.type} chart placeholder]</div>
    </div>
  );
}

export default function Home() {
  const [dashboardConfig, setDashboardConfig] = useState<DashboardConfig | null>(null);
  useEffect(() => {
    import("../dashboard-config.json").then(mod => setDashboardConfig(mod.default || mod));
  }, []);

  if (!dashboardConfig) return <div>Loading dashboard...</div>;

  return (
    <div className="min-h-screen bg-gray-50 p-8">
      <div className="flex flex-col md:flex-row gap-8">
        {/* Sidebar KPI panels */}
        <div className="flex flex-col gap-4 w-full md:w-1/5">
          {dashboardConfig.kpiPanels.map(panel => (
            <KpiPanel key={panel.id} config={panel} />
          ))}
        </div>
        {/* Main dashboard area */}
        <div className="flex-1 flex flex-col gap-6">
          <FilterBar configs={dashboardConfig.filters} />
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {dashboardConfig.mainPanels.map(panel => (
              <MainPanel key={panel.id} config={panel} />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
