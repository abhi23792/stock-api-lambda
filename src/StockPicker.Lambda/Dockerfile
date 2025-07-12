FROM public.ecr.aws/lambda/dotnet:8

WORKDIR /var/task

# Copy published output from your build
COPY bin/Release/net8.0/publish/ .

# Set the Lambda handler
CMD ["StockPicker.Lambda::StockPicker.Lambda.Function::Handle"]