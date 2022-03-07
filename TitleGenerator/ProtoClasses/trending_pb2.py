# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: trending.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()


import ProtoClasses.video_pb2 as video__pb2


DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x0etrending.proto\x1a\x0bvideo.proto\"%\n\x0fTrendingRequest\x12\x12\n\ntrendingId\x18\x01 \x01(\t\"3\n\x12TrendingPageVideos\x12\x1d\n\tVideoData\x18\x01 \x03(\x0b\x32\n.VideoData\"D\n\x17MultipleTrendingRequest\x12)\n\x0fTrendingRequest\x18\x01 \x03(\x0b\x32\x10.TrendingRequest2\x92\x01\n\x08Trending\x12:\n\x11GetTrendingVideos\x12\x10.TrendingRequest\x1a\x13.TrendingPageVideos\x12J\n\x19GetMultipleTrendingVideos\x12\x18.MultipleTrendingRequest\x1a\x13.TrendingPageVideosb\x06proto3')



_TRENDINGREQUEST = DESCRIPTOR.message_types_by_name['TrendingRequest']
_TRENDINGPAGEVIDEOS = DESCRIPTOR.message_types_by_name['TrendingPageVideos']
_MULTIPLETRENDINGREQUEST = DESCRIPTOR.message_types_by_name['MultipleTrendingRequest']
TrendingRequest = _reflection.GeneratedProtocolMessageType('TrendingRequest', (_message.Message,), {
  'DESCRIPTOR' : _TRENDINGREQUEST,
  '__module__' : 'trending_pb2'
  # @@protoc_insertion_point(class_scope:TrendingRequest)
  })
_sym_db.RegisterMessage(TrendingRequest)

TrendingPageVideos = _reflection.GeneratedProtocolMessageType('TrendingPageVideos', (_message.Message,), {
  'DESCRIPTOR' : _TRENDINGPAGEVIDEOS,
  '__module__' : 'trending_pb2'
  # @@protoc_insertion_point(class_scope:TrendingPageVideos)
  })
_sym_db.RegisterMessage(TrendingPageVideos)

MultipleTrendingRequest = _reflection.GeneratedProtocolMessageType('MultipleTrendingRequest', (_message.Message,), {
  'DESCRIPTOR' : _MULTIPLETRENDINGREQUEST,
  '__module__' : 'trending_pb2'
  # @@protoc_insertion_point(class_scope:MultipleTrendingRequest)
  })
_sym_db.RegisterMessage(MultipleTrendingRequest)

_TRENDING = DESCRIPTOR.services_by_name['Trending']
if _descriptor._USE_C_DESCRIPTORS == False:

  DESCRIPTOR._options = None
  _TRENDINGREQUEST._serialized_start=31
  _TRENDINGREQUEST._serialized_end=68
  _TRENDINGPAGEVIDEOS._serialized_start=70
  _TRENDINGPAGEVIDEOS._serialized_end=121
  _MULTIPLETRENDINGREQUEST._serialized_start=123
  _MULTIPLETRENDINGREQUEST._serialized_end=191
  _TRENDING._serialized_start=194
  _TRENDING._serialized_end=340
# @@protoc_insertion_point(module_scope)
