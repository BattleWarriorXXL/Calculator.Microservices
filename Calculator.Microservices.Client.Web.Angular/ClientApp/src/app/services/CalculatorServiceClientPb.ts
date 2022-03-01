/**
 * @fileoverview gRPC-Web generated client stub for Calculator
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!


/* eslint-disable */
// @ts-nocheck


import * as grpcWeb from 'grpc-web';

import * as calculator_pb from './calculator_pb';


export class CalculatorClient {
  client_: grpcWeb.AbstractClientBase;
  hostname_: string;
  credentials_: null | { [index: string]: string; };
  options_: null | { [index: string]: any; };

  constructor (hostname: string,
               credentials?: null | { [index: string]: string; },
               options?: null | { [index: string]: any; }) {
    if (!options) options = {};
    if (!credentials) credentials = {};
    options['format'] = 'binary';

    this.client_ = new grpcWeb.GrpcWebClientBase(options);
    this.hostname_ = hostname;
    this.credentials_ = credentials;
    this.options_ = options;
  }

  methodDescriptorAdd = new grpcWeb.MethodDescriptor(
    '/Calculator.Calculator/Add',
    grpcWeb.MethodType.UNARY,
    calculator_pb.AddRequest,
    calculator_pb.ResultResponse,
    (request: calculator_pb.AddRequest) => {
      return request.serializeBinary();
    },
    calculator_pb.ResultResponse.deserializeBinary
  );

  add(
    request: calculator_pb.AddRequest,
    metadata: grpcWeb.Metadata | null): Promise<calculator_pb.ResultResponse>;

  add(
    request: calculator_pb.AddRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void): grpcWeb.ClientReadableStream<calculator_pb.ResultResponse>;

  add(
    request: calculator_pb.AddRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Calculator.Calculator/Add',
        request,
        metadata || {},
        this.methodDescriptorAdd,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Calculator.Calculator/Add',
    request,
    metadata || {},
    this.methodDescriptorAdd);
  }

  methodDescriptorSubtract = new grpcWeb.MethodDescriptor(
    '/Calculator.Calculator/Subtract',
    grpcWeb.MethodType.UNARY,
    calculator_pb.SubtractRequest,
    calculator_pb.ResultResponse,
    (request: calculator_pb.SubtractRequest) => {
      return request.serializeBinary();
    },
    calculator_pb.ResultResponse.deserializeBinary
  );

  subtract(
    request: calculator_pb.SubtractRequest,
    metadata: grpcWeb.Metadata | null): Promise<calculator_pb.ResultResponse>;

  subtract(
    request: calculator_pb.SubtractRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void): grpcWeb.ClientReadableStream<calculator_pb.ResultResponse>;

  subtract(
    request: calculator_pb.SubtractRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Calculator.Calculator/Subtract',
        request,
        metadata || {},
        this.methodDescriptorSubtract,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Calculator.Calculator/Subtract',
    request,
    metadata || {},
    this.methodDescriptorSubtract);
  }

  methodDescriptorMultiply = new grpcWeb.MethodDescriptor(
    '/Calculator.Calculator/Multiply',
    grpcWeb.MethodType.UNARY,
    calculator_pb.MultiplyRequest,
    calculator_pb.ResultResponse,
    (request: calculator_pb.MultiplyRequest) => {
      return request.serializeBinary();
    },
    calculator_pb.ResultResponse.deserializeBinary
  );

  multiply(
    request: calculator_pb.MultiplyRequest,
    metadata: grpcWeb.Metadata | null): Promise<calculator_pb.ResultResponse>;

  multiply(
    request: calculator_pb.MultiplyRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void): grpcWeb.ClientReadableStream<calculator_pb.ResultResponse>;

  multiply(
    request: calculator_pb.MultiplyRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Calculator.Calculator/Multiply',
        request,
        metadata || {},
        this.methodDescriptorMultiply,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Calculator.Calculator/Multiply',
    request,
    metadata || {},
    this.methodDescriptorMultiply);
  }

  methodDescriptorDivide = new grpcWeb.MethodDescriptor(
    '/Calculator.Calculator/Divide',
    grpcWeb.MethodType.UNARY,
    calculator_pb.DivideRequest,
    calculator_pb.ResultResponse,
    (request: calculator_pb.DivideRequest) => {
      return request.serializeBinary();
    },
    calculator_pb.ResultResponse.deserializeBinary
  );

  divide(
    request: calculator_pb.DivideRequest,
    metadata: grpcWeb.Metadata | null): Promise<calculator_pb.ResultResponse>;

  divide(
    request: calculator_pb.DivideRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void): grpcWeb.ClientReadableStream<calculator_pb.ResultResponse>;

  divide(
    request: calculator_pb.DivideRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Calculator.Calculator/Divide',
        request,
        metadata || {},
        this.methodDescriptorDivide,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Calculator.Calculator/Divide',
    request,
    metadata || {},
    this.methodDescriptorDivide);
  }

  methodDescriptorPow = new grpcWeb.MethodDescriptor(
    '/Calculator.Calculator/Pow',
    grpcWeb.MethodType.UNARY,
    calculator_pb.PowRequest,
    calculator_pb.ResultResponse,
    (request: calculator_pb.PowRequest) => {
      return request.serializeBinary();
    },
    calculator_pb.ResultResponse.deserializeBinary
  );

  pow(
    request: calculator_pb.PowRequest,
    metadata: grpcWeb.Metadata | null): Promise<calculator_pb.ResultResponse>;

  pow(
    request: calculator_pb.PowRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void): grpcWeb.ClientReadableStream<calculator_pb.ResultResponse>;

  pow(
    request: calculator_pb.PowRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Calculator.Calculator/Pow',
        request,
        metadata || {},
        this.methodDescriptorPow,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Calculator.Calculator/Pow',
    request,
    metadata || {},
    this.methodDescriptorPow);
  }

  methodDescriptorSqrt = new grpcWeb.MethodDescriptor(
    '/Calculator.Calculator/Sqrt',
    grpcWeb.MethodType.UNARY,
    calculator_pb.SqrtRequest,
    calculator_pb.ResultResponse,
    (request: calculator_pb.SqrtRequest) => {
      return request.serializeBinary();
    },
    calculator_pb.ResultResponse.deserializeBinary
  );

  sqrt(
    request: calculator_pb.SqrtRequest,
    metadata: grpcWeb.Metadata | null): Promise<calculator_pb.ResultResponse>;

  sqrt(
    request: calculator_pb.SqrtRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void): grpcWeb.ClientReadableStream<calculator_pb.ResultResponse>;

  sqrt(
    request: calculator_pb.SqrtRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: calculator_pb.ResultResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/Calculator.Calculator/Sqrt',
        request,
        metadata || {},
        this.methodDescriptorSqrt,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/Calculator.Calculator/Sqrt',
    request,
    metadata || {},
    this.methodDescriptorSqrt);
  }

}

