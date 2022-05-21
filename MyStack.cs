using docker_example;
using Pulumi;

class MyStack : Stack
{
    public MyStack()
    {
        
        var stackName = Deployment.Instance.StackName;

        if (stackName == "prod")
        {
            new OurDocker(stackName, new OurDocker.OurDockerArgs
            {
                ExternalPort = 8080,
                InternalPort = 80
            });
        }
        else
        {
            Pulumi.Log.Error("Deploying to wrong stack; check stack configuration.");
        }
        
    }
}
