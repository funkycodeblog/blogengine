export interface ServiceResponse<T = null>
{
    Data: T;
    Status: number;
    StatusText: string;

}

export function createServiceResponse<T>(data: T, status: number, statusText: string ) : ServiceResponse<T>
{
    return {
        Data: data,
        Status: status,
        StatusText: statusText
    };

}

export function createServiceResponseNoData(status: number, statusText: string ) : ServiceResponse
{
    return {
        Data: null,
        Status: status,
        StatusText: statusText
    };

}