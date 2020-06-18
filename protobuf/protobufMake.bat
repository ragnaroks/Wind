protoc.exe --csharp_out=. PipelineMessage.proto
protoc.exe --csharp_out=. WebSocketMessages.proto
protoc.exe --js_out=import_style=commonjs,binary:. WebSocketMessages.proto