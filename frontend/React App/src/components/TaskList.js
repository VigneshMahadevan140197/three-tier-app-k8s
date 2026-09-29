import React, { useState, useEffect } from 'react';

function TaskList() {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    fetch('/api/tasks')
      .then(res => res.json())
      .then(data => setTasks(data));
  }, []);

  return (
    <ul>
      {tasks.map(task => (
        <li key={task.id}>
          {task.title} - {task.completed ? "Done" : "Pending"}
        </li>
      ))}
    </ul>
  );
}

export default TaskList;
