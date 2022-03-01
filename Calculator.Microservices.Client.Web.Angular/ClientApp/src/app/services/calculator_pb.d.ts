import * as jspb from 'google-protobuf'



export class AddRequest extends jspb.Message {
  getA(): number;
  setA(value: number): AddRequest;

  getB(): number;
  setB(value: number): AddRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AddRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AddRequest): AddRequest.AsObject;
  static serializeBinaryToWriter(message: AddRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AddRequest;
  static deserializeBinaryFromReader(message: AddRequest, reader: jspb.BinaryReader): AddRequest;
}

export namespace AddRequest {
  export type AsObject = {
    a: number,
    b: number,
  }
}

export class SubtractRequest extends jspb.Message {
  getA(): number;
  setA(value: number): SubtractRequest;

  getB(): number;
  setB(value: number): SubtractRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SubtractRequest.AsObject;
  static toObject(includeInstance: boolean, msg: SubtractRequest): SubtractRequest.AsObject;
  static serializeBinaryToWriter(message: SubtractRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SubtractRequest;
  static deserializeBinaryFromReader(message: SubtractRequest, reader: jspb.BinaryReader): SubtractRequest;
}

export namespace SubtractRequest {
  export type AsObject = {
    a: number,
    b: number,
  }
}

export class MultiplyRequest extends jspb.Message {
  getA(): number;
  setA(value: number): MultiplyRequest;

  getB(): number;
  setB(value: number): MultiplyRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): MultiplyRequest.AsObject;
  static toObject(includeInstance: boolean, msg: MultiplyRequest): MultiplyRequest.AsObject;
  static serializeBinaryToWriter(message: MultiplyRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): MultiplyRequest;
  static deserializeBinaryFromReader(message: MultiplyRequest, reader: jspb.BinaryReader): MultiplyRequest;
}

export namespace MultiplyRequest {
  export type AsObject = {
    a: number,
    b: number,
  }
}

export class DivideRequest extends jspb.Message {
  getA(): number;
  setA(value: number): DivideRequest;

  getB(): number;
  setB(value: number): DivideRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DivideRequest.AsObject;
  static toObject(includeInstance: boolean, msg: DivideRequest): DivideRequest.AsObject;
  static serializeBinaryToWriter(message: DivideRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DivideRequest;
  static deserializeBinaryFromReader(message: DivideRequest, reader: jspb.BinaryReader): DivideRequest;
}

export namespace DivideRequest {
  export type AsObject = {
    a: number,
    b: number,
  }
}

export class PowRequest extends jspb.Message {
  getX(): number;
  setX(value: number): PowRequest;

  getY(): number;
  setY(value: number): PowRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PowRequest.AsObject;
  static toObject(includeInstance: boolean, msg: PowRequest): PowRequest.AsObject;
  static serializeBinaryToWriter(message: PowRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PowRequest;
  static deserializeBinaryFromReader(message: PowRequest, reader: jspb.BinaryReader): PowRequest;
}

export namespace PowRequest {
  export type AsObject = {
    x: number,
    y: number,
  }
}

export class SqrtRequest extends jspb.Message {
  getD(): number;
  setD(value: number): SqrtRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SqrtRequest.AsObject;
  static toObject(includeInstance: boolean, msg: SqrtRequest): SqrtRequest.AsObject;
  static serializeBinaryToWriter(message: SqrtRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SqrtRequest;
  static deserializeBinaryFromReader(message: SqrtRequest, reader: jspb.BinaryReader): SqrtRequest;
}

export namespace SqrtRequest {
  export type AsObject = {
    d: number,
  }
}

export class ResultResponse extends jspb.Message {
  getResult(): number;
  setResult(value: number): ResultResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ResultResponse.AsObject;
  static toObject(includeInstance: boolean, msg: ResultResponse): ResultResponse.AsObject;
  static serializeBinaryToWriter(message: ResultResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ResultResponse;
  static deserializeBinaryFromReader(message: ResultResponse, reader: jspb.BinaryReader): ResultResponse;
}

export namespace ResultResponse {
  export type AsObject = {
    result: number,
  }
}

