import { FunkyActionTypes } from './ActionDefinitions'
import { ErrorInfo } from '../model/ErrorInfo';




// ----------------------------------------------------------------------------------------
export interface ISetLoadingStatus {
  type: FunkyActionTypes.SET_LOADING_STATUS,
  isLoading: boolean
}

export function setLoadingStatusAction(isLoading: boolean): ISetLoadingStatus {
  return {
    type: FunkyActionTypes.SET_LOADING_STATUS,
    isLoading: isLoading
  }
}

export interface ISetErrorInfoAction {
   type : FunkyActionTypes.SET_ERROR_INFO,
   error: ErrorInfo
};

export function setErrorInfoAction(errorInfo: ErrorInfo) : ISetErrorInfoAction {
  return {
    type : FunkyActionTypes.SET_ERROR_INFO,
    error: errorInfo
  }
}

