
using System;
using System.Collections.Generic;

abstract class DeploymentPipeline
{
    public string Environment { get; protected set; }
    public bool IsSuccessful { get; protected set; }

    protected DeploymentPipeline(string environment)
    {
        Environment = environment;
        IsSuccessful = false;
    }

    public void Deploy()
    {
        Console.WriteLine($"\n=== {Environment} Deployment ===");

        Build();
        RunTests();
        Package();
        Upload();
        Notify();

        Console.WriteLine($"Result: {(IsSuccessful ? "Success" : "Completed")}");
    }

    protected abstract void Build();

    protected abstract void RunTests();

    protected virtual void Package()
    {
        Console.WriteLine("Creating deployment package...");
    }

    protected abstract void Upload();

    protected virtual void Notify()
    {
        Console.WriteLine($"Deployment to {Environment} finished.");
        IsSuccessful = true;
    }
}

class StagingDeploy : DeploymentPipeline
{
    public StagingDeploy() : base("Staging")
    {
    }

    protected override void Build()
    {
        Console.WriteLine("Building application for testing...");
    }

    protected override void RunTests()
    {
        Console.WriteLine("Running tests on staging...");
    }

    protected override void Upload()
    {
        Console.WriteLine("Uploading to the test server...");
    }
}

class ProductionDeploy : DeploymentPipeline
{
    public ProductionDeploy() : base("Production")
    {
    }

    protected override void Build()
    {
        Console.WriteLine("Building release version...");
    }

    protected override void RunTests()
    {
        Console.WriteLine("Running all production tests...");
    }

    protected override void Package()
    {
        Console.WriteLine("Creating optimized release package...");
    }

    protected override void Upload()
    {
        Console.WriteLine("Uploading to the production server...");
    }

    protected override void Notify()
    {
        Console.WriteLine("Sending production deployment notification...");
        IsSuccessful = true;
    }
}

class RollbackDeploy : DeploymentPipeline
{
    public RollbackDeploy() : base("Rollback")
    {
    }

    protected override void Build()
    {
        Console.WriteLine("Preparing the previous stable version...");
    }

    protected override void RunTests()
    {
        Console.WriteLine("Checking the previous version...");
    }

    protected override void Package()
    {
        Console.WriteLine("Preparing the rollback package...");
    }

    protected override void Upload()
    {
        Console.WriteLine("Restoring the previous version on the server...");
    }
}

class Program
{
    static void Main()
    {
        List<DeploymentPipeline> pipelines = new List<DeploymentPipeline>
        {
            new StagingDeploy(),
            new ProductionDeploy(),
            new RollbackDeploy()
        };

        foreach (DeploymentPipeline pipeline in pipelines)
        {
            pipeline.Deploy();
        }
    }
}
