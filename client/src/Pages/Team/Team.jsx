import React, { useEffect, useState } from "react";
import teamService from "../../Service/teamService";
import TeamList from "../../Components/Team/TeamList";
import TeamAdd from "../../Components/Team/TeamAdd";

function Team() {
  return (
    <div>
      <TeamAdd />
      <TeamList />
    </div>
  );
}

export default Team;
