export class Ticket
{
  id : number | undefined;
  title : string | undefined;
  description : string | undefined;
  created : Date| undefined;
  updated : Date | undefined;
  ticketTypeName : string | undefined;
  ticketColor : string | undefined ;
  ticketTypeId:number | undefined ;
  stateColor : string | undefined ;
  stateName : string | undefined ;
  stateId : number | undefined ;
  assignedToName:string | undefined ;
  assignedToId : number | undefined ;
  moduleName : string | undefined ;
  subMenuName : string | undefined ;
  subMenuId : number | undefined ;
  moduleId : number | undefined ;
}

export class TicketState{
  id : number | undefined;
  name : string | undefined;
  description : string | undefined;
  color : string | undefined;
}

export class TicketType{
  id : number | undefined;
  name : string| undefined;
  icon : string | undefined;
}
