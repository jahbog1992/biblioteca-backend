﻿namespace Biblioteca.DTO.Response
{
    public class BaseResponseGeneric<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
