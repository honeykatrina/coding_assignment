﻿using System.Net;
using UserAccountManagement.Shared.Models;

namespace UserAccountManagement.Shared.Helpers;

public class BaseResponseBuilder<T>
{
    private T _model;
    private Error _error;
    private bool _success;

    public BaseResponseBuilder<T> SetSuccess()
    {
        _success = true;
        return this;
    }
    
    public BaseResponseBuilder<T> SetError(Error error)
    {
        _error = error;
        _success = false;

        return this;
    }

    public BaseResponseBuilder<T> SetModel(T model)
    {
        _model = model;
        return this;
    }

    public BaseResponse<T> Build()
    {
        return new BaseResponse<T>(_model, _success, _error);
    }

    public BaseResponse<T> BuildSuccessResponse(T model)
    {
        return new BaseResponseBuilder<T>()
            .SetSuccess()
            .SetModel(model)
            .Build();
    }

    public BaseResponse<T> BuildErrorResponse(HttpStatusCode code, string message)
    {
        return new BaseResponseBuilder<T>()
                .SetError(new()
                { ErrorCode = code, Message = message })
                .Build();
    }
}
