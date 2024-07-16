import React, { useEffect, useState } from 'react';
import { SensorData } from '../types/SensorData';

const Dashboard: React.FC = () => {
  const [sensorData, setSensorData] = useState<SensorData | null>(null);

  useEffect(() => {
    // TODO: Implement API call to fetch sensor data
    // Update sensorData state with the fetched data
  }, []);

  return (
    <div>
      <h2>Hydroponics Dashboard</h2>
      {sensorData ? (
        <div>
          <p>pH: {sensorData.ph}</p>
          <p>EC: {sensorData.ec}</p>
          <p>Temperature: {sensorData.temperature}°C</p>
          <p>Humidity: {sensorData.humidity}%</p>
        </div>
      ) : (
        <p>Loading sensor data...</p>
      )}
    </div>
  );
};

export default Dashboard;
