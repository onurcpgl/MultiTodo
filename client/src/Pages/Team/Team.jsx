import React, { useEffect, useState } from "react";
import todoService from "../../Service/todoService";

function Team() {
  const [todo, setTodo] = useState(null);
  useEffect(() => {
    (async () => {
      const result = await todoService.getTodo();
      console.log(result);
      setTodo(result);
    })();
  }, []);
  return (
    <div>
      merhaba
      {/* {todo?.map((item, i) => (
        <div key={i}>{item.title}</div>
      ))} */}
    </div>
  );
}

export default Team;
