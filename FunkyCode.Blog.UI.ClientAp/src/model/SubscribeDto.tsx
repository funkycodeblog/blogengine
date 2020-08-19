import { SubscribeDataActionType } from "./SubscribeDataActionType";

export interface SubscribeDto {
  username: string;
  email: string;
  action: SubscribeDataActionType;

}
