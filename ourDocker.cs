using Pulumi;
using Pulumi.Docker.Inputs;
using Docker = Pulumi.Docker;

namespace docker_example
{
    public class OurDocker : ComponentResource
    {
        public OurDocker(string name, OurDockerArgs args, 
            ComponentResourceOptions? options = null, bool remote = false) :
            base("dockerexample:demo:container", name, args, options, remote)
        {
            // Find the latest Ubuntu precise image.
            var remoteImage = new Docker.RemoteImage(name, new Docker.RemoteImageArgs
            {
                Name = args.ImageName,
            });
            // Start a container
            var ubuntuContainer = new Docker.Container("ubuntuContainer", new Docker.ContainerArgs
            {
                Name = args.ContainerName,
                Image = remoteImage.Latest,
                Ports = new ContainerPortArgs
                {
                    External = args.ExternalPort,
                    Internal = args.InternalPort,
                }
            });
        }
        public sealed class OurDockerArgs : ResourceArgs
        {
            [Input("containerName")] 
            public Input<string> ContainerName { get; set; } = null!;
            
            [Input("imageName")] 
            public Input<string> ImageName { get; set; } = "nginx:1.21.3-alpine";
            
            [Input("externalPort")] 
            public Input<int> ExternalPort { get; set; } = null!;
            
            [Input("internalPort")] 
            public Input<int> InternalPort { get; set; } = null!;
        }
    }
}