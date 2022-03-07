from time import time
from tracemalloc import stop
import grpc
import ProtoClasses.trending_pb2 as trending_pb2
import ProtoClasses.trending_pb2_grpc as trending_pb2_grpc
import ProtoClasses.video_pb2 as video_pb2
import ProtoClasses.video_pb2_grpc as video_pb2_grpc
import timeit
import requests
import warnings
import sys
import base64
warnings.filterwarnings("ignore")


def test_grpc(numberOfTimes):
    start = timeit.default_timer()
    for _ in range(numberOfTimes):
        channel = grpc.insecure_channel("localhost:5117")
        stub = video_pb2_grpc.VideoStub(channel)
        response = stub.GetVideoInfo(
            video_pb2.VideoRequest(videoId="_Iye7890tqQ"))
    stop = timeit.default_timer()
    return stop - start


def test_rest(numberOfTimes):
    start = timeit.default_timer()
    for _ in range(numberOfTimes):
        response = requests.get(
            f"https://localhost:7244/api/Video/_Iye7890tqQ", verify=False)
    stop = timeit.default_timer()
    return stop - start


def test_both(test_grpc, test_rest):
    print("Testing grpc:")
    grpcTime = test_grpc(100)
    print(f'The grpc time was: {grpcTime} \n')

    print("Testing rest:")
    restTime = test_rest(100)
    print(f'The rest time was: {restTime}')

    compare_times(grpcTime, restTime)

def compare_times(grpcTime, restTime):
    if grpcTime < restTime:
        print(f"grpc was faster by: {restTime - grpcTime} seconds")
    elif grpcTime > restTime:
        print(f"rest was faster by: {grpcTime - restTime} seconds")
    else:
        print("It took the same time")


if __name__ == '__main__':
    channel = grpc.insecure_channel("localhost:5117")
    stub2 = trending_pb2_grpc.TrendingStub(channel)
    
    response2 = stub2.GetMultipleTrendingVideos(trending_pb2.MultipleTrendingRequest(TrendingRequest =
                                                                                                    [trending_pb2.TrendingRequest(trendingId = 'US'), trending_pb2.TrendingRequest(trendingId = 'CA')]))
    
    print(response2)
