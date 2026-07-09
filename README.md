# Figma MCP Server

A robust, production-ready Model Context Protocol (MCP) server that allows AI agents to interact directly with Figma files and nodes, fetch design data, and export images.

## Features
- **Fetch Design JSON:** Extract raw node data from Figma files for AI consumption.
- **Export Images:** Render any Figma node/frame to PNG, JPG, SVG, or PDF directly from the AI.
- **URL Parsing:** Simply pass a full Figma URL — the server automatically extracts the `fileKey` and `nodeId` for you.
- **Multi-Tenant Token Support:** Allows tokens to be passed globally (server-wide) or injected dynamically per-call, enabling safe multi-user or multi-agent usage.

## Setup & Installation

This server is published as a **Self-Contained Single-File Executable**, meaning it runs out-of-the-box on Windows without needing the .NET SDK installed.

1. Locate the executable file: `publish\FigmaMCP.Core.exe` (or wherever you extracted the release).
2. Add the server to your MCP Client configuration (see below).

## Configuration (MCP Clients)

You can configure this server in Claude Desktop, Cursor, or any other MCP-compatible client. Add the following to your `mcpServers` JSON config file:

### Option 1: Global Token (Single-User)
If you are the only person using this client, you can configure your Personal Access Token once via an environment variable. The AI will not need to pass the token manually.

```json
{
  "mcpServers": {
    "figma-mcp": {
      "command": "C:\\path\\to\\publish\\FigmaMCP.Core.exe",
      "env": {
        "FIGMA__TOKEN": "figd_YourPersonalAccessTokenHere"
      }
    }
  }
}
```

### Option 2: Dynamic Token (Multi-User / Hosted)
If you want the AI to prompt the user for their token or use different tokens for different tasks, omit the environment variable. The AI will pass the `figmaAccessToken` as an argument to every tool call.

```json
{
  "mcpServers": {
    "figma-mcp": {
      "command": "C:\\path\\to\\publish\\FigmaMCP.Core.exe"
    }
  }
}
```

## Available Tools

Once connected, your AI client will automatically have access to the following tools:

### `get_figma_design`
Fetches the JSON representation of a Figma file or a specific node.
- **Arguments:**
  - `figmaUrl` (optional): The full Figma URL (e.g., `https://www.figma.com/design/ABC123/File?node-id=1-2`). Takes priority if provided.
  - `fileKey` (optional): The ID of the file (used if `figmaUrl` is not provided).
  - `nodeId` (optional): The ID of the specific node/frame.
  - `figmaAccessToken` (optional): Required only if `FIGMA__TOKEN` is not set in the environment.

### `export_figma_image`
Exports a specific Figma node as an image.
- **Arguments:**
  - `figmaUrl` (optional): The full Figma URL. Takes priority if provided.
  - `fileKey` (optional): The ID of the file (used if `figmaUrl` is not provided).
  - `nodeId` (optional): The ID of the specific node/frame to render. (Required)
  - `format` (optional): The image format (`png`, `jpg`, `svg`, `pdf`). Defaults to `png`.
  - `figmaAccessToken` (optional): Required only if `FIGMA__TOKEN` is not set in the environment.

## Troubleshooting

- **429 (Too Many Requests):** Figma aggressively rate-limits their `/files` and `/nodes` API endpoints. If you receive a `429` error, you must wait (usually 60+ seconds) before Figma will accept requests from your token again.
- **401/403 Unauthorized:** Ensure your Figma Personal Access Token is correct and has the appropriate read permissions for the file you are trying to access.
