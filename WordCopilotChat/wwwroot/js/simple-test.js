// 简单测试脚本
console.log('🧪 简单测试脚本已加载');

// 测试现代化工具预览
function testSimplePreview() {
    console.log('🔧 开始测试简单预览');
    
    // 模拟工具预览数据
    const testData = {
        success: true,
        preview_mode: true,
        action_type: 'insert_content',
        target_heading: '测试标题',
        format_type: 'paragraph',
        indent_level: 0,
        add_spacing: true,
        original_content: '这是一段测试内容',
        preview_content: '这是预览内容，用于测试现代化预览界面的显示效果。',
        message: '将在指定位置插入段落内容'
    };
    
    // 生成预览ID
    const previewId = 'test-preview-' + Date.now();
    
    // 生成预览HTML
    const previewHtml = generateModernToolPreview(testData, previewId);
    
    // 添加到页面
    const testContainer = document.getElementById('test-container') || createSimpleTestContainer();
    testContainer.innerHTML = previewHtml;
    
    // 绑定事件
    const previewElement = testContainer.querySelector('.tool-preview-container');
    if (previewElement) {
        bindToolPreviewEvents(previewElement, previewId, {
            action_type: testData.action_type,
            parameters: {
                target_heading: testData.target_heading,
                content: testData.original_content,
                format_type: testData.format_type,
                indent_level: testData.indent_level,
                add_spacing: testData.add_spacing
            }
        });
        
        console.log('✅ 预览测试完成');
        console.log('📝 测试说明：');
        console.log('- 点击预览信息行可以展开/折叠预览内容');
        console.log('- 预览内容默认是折叠状态');
        console.log('- 右侧有展开按钮 ▼/▲');
        console.log('- 按钮文字已改为"接受"和"拒绝"，按钮宽度已调整');
        console.log('- 点击"接受"后会移除"拒绝"按钮，只保留不可点击的"接受"状态');
        console.log('- 点击"拒绝"后预览会被移除');
        console.log('- 当所有预览处理完成后，批量操作弹窗会立即自动消失');
    } else {
        console.error('❌ 预览元素未找到');
    }
}

// 测试ReAct内容解析
function testSimpleReAct() {
    console.log('🤔 测试ReAct内容解析');
    
    const testContent = `
    <THINKING>
    用户需要在特定标题下插入内容。我需要分析：
    1. 目标位置：测试标题下方
    2. 内容类型：段落文本
    3. 格式要求：标准段落格式
    </THINKING>
    
    <OBSERVATION>
    - 目标标题："测试标题"
    - 插入位置：标题下方
    - 内容格式：段落文本
    </OBSERVATION>
    
    <ACTION>
    我将使用工具在指定位置插入段落内容。
    </ACTION>
    
    现在开始执行操作。
    `;
    
    const parsedContent = parseReActContent(testContent);
    console.log('解析结果:', parsedContent);
    
    // 显示在页面上
    const testContainer = document.getElementById('test-container') || createSimpleTestContainer();
    testContainer.innerHTML = `
        <h4>🧪 ReAct内容解析测试</h4>
        <div style="background: #f8fafc; padding: 16px; border-radius: 8px; border: 1px solid #e2e8f0;">
            ${parsedContent}
        </div>
    `;
    
    console.log('✅ ReAct解析测试完成');
}

// 创建简单的测试容器
function createSimpleTestContainer() {
    const existing = document.getElementById('test-container');
    if (existing) {
        existing.remove();
    }
    
    const container = document.createElement('div');
    container.id = 'test-container';
    container.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        width: 400px;
        max-height: 600px;
        background: white;
        border: 2px solid #3b82f6;
        border-radius: 12px;
        padding: 16px;
        box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
        z-index: 10000;
        overflow-y: auto;
    `;
    
    const header = document.createElement('div');
    header.innerHTML = `
        <h3 style="margin: 0 0 12px 0; color: #3b82f6;">🧪 测试面板</h3>
        <button onclick="this.parentElement.parentElement.remove()" style="
            position: absolute; top: 10px; right: 10px; 
            background: #ef4444; color: white; border: none; 
            border-radius: 50%; width: 24px; height: 24px; 
            cursor: pointer; font-size: 12px;
        ">×</button>
    `;
    
    container.appendChild(header);
    document.body.appendChild(container);
    return container;
}

// 立即导出到全局作用域
window.testSimplePreview = testSimplePreview;
window.testSimpleReAct = testSimpleReAct;

console.log('✅ 简单测试函数已导出到全局作用域');
console.log('可用函数: testSimplePreview(), testSimpleReAct()'); 